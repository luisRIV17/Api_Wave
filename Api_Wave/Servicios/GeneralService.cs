using Api_Wave.Models;
using Api_Wave.Models.ViewModels;

namespace Api_Wave.Servicios
{
    public class GeneralService:IGeneralService
    {
        private readonly ChatwaveContext milinq;
        public GeneralService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }

      
        //obtener datos para las fichas de los mensjes
        public ModelDatossalas datossalas(string idsala, string idpersona)
        {
            var dato2 =( from f in milinq.IntegrantesSalas
                        join m in milinq.Mensajes on f.IdSala equals m.IdSala
                        where f.IdSala == idsala
                        orderby m.FechaMensaje descending
                        select new
                        {
                            idmensaje=m.IdMensaje,
                            nombresala = f.IdSalaNavigation.NombreSala,
                            ultimome = /*m.Imagen.ToString() ??*/ m.Mensaje1 ?? m.Archivo.ToString() ?? m.Audio.ToString(),//selecciona el que no sea null
                            fecha = m.FechaMensaje.ToString("d/M/yyyy"),
                            hora = m.FechaMensaje.ToString("hh:mm tt"),
                            tipochat=f.IdSalaNavigation.IdTipoSala,
                            envia=m.IdIntegranteNavigation.IdPersona == idpersona ? true : false//true si es la misma persona que envio el ultimo mensaje, false si es otra persona que lo envio
                        }).FirstOrDefault();
            ModelDatossalas md = new ModelDatossalas
            {
                nombresala = dato2.nombresala,
                ultimomensaje = dato2.ultimome,
                fecha = dato2.fecha,
                hora = dato2.hora,
                envia = dato2.envia,
            };
            if (dato2.envia==false)
            {
                var inte =(from i in milinq.IntegrantesSalas
                          where i.IdPersona ==idpersona && i.IdSala==idsala
                          select i.IdIntegrante).FirstOrDefault();
                var verificalectura = (from v in milinq.EstadoMensajes
                                      join d in milinq.DetalleEstadoMensajes on v.IdEstado equals d.IdEstado
                                      where v.IdMensaje == dato2.idmensaje && d.IdIntegrante == inte
                                      select new
                                      {
                                          lectura =d.IdTipoLectura
                                      }).FirstOrDefault();

                int cantidadMen = milinq.DetalleEstadoMensajes
       .Count(f => f.IdIntegrante == inte && f.IdTipoLectura!=4);
                md.cant = cantidadMen;
              
               
                md.tipolec = verificalectura != null ? Convert.ToInt32(verificalectura.lectura) : 0;
            }
           
          

            if(dato2.tipochat==1)
            {
                var idcontacto = (from f in milinq.IntegrantesSalas

                                  where f.IdSala == idsala && f.IdPersona != idpersona
                                  select new
                                  {
                                      idpersonav= f.IdPersona,
                                      idpersona = f.IdPersonaNavigation.Nombre + " " + f.IdPersonaNavigation.Apellido
                                  }).FirstOrDefault();

                var contacto = from p in milinq.PersonaUsuarios
                               join c in milinq.Contactos on p.IdUsuario equals c.IdUsuario
                               where p.IdPersona == idpersona && c.UsuarioContacto== idcontacto.idpersonav 
                               select c.AliasContacto;

                if (contacto.FirstOrDefault() == null)
                    md.nombresala = idcontacto.idpersona;
                else
                    md.nombresala = contacto.FirstOrDefault();
            }
            else
            {
                var idcontacto = (from f in milinq.Salas
                                  where f.IdSala == idsala 
                                  select new
                                  {
                                      idpersona = f.NombreSala
                                  }).FirstOrDefault();
                md.nombresala = idcontacto.idpersona;
            }

            return md;
                        
        }
        //obtiene solo el listado de los salas con sus codigos
        public List<ModelMPrincipal> inicio(string idpersona)
        {
            var salas = (from s in milinq.Salas
                         join p in milinq.IntegrantesSalas on s.IdSala equals p.IdSala
                         join m in milinq.Mensajes on s.IdSala equals m.IdSala
                         where p.IdPersona == idpersona && s.EstadoChat == true
                         group new { s, m } by new ModelMPrincipal
                         {
                             id_sala = s.IdSala,
                             idintengrante = p.IdIntegrante,
                         }
              into gru
                         select new
                         {
                             gru.Key.id_sala,
                             gru.Key.idintengrante,
                             ultimaFechaMensaje = gru.Max(x => x.m.FechaMensaje) // Obtén la fecha máxima dentro del grupo
                         })
              .ToList() // Ejecutar la consulta y obtener los resultados en una lista
              .OrderByDescending(x => x.ultimaFechaMensaje)  // Ordena los resultados por la fecha máxima
              .Select(x => new ModelMPrincipal
              {
                  id_sala = x.id_sala,
                  idintengrante = x.idintengrante
              })
              .ToList();





            return salas;

        }
        //Crear nueva sala o redirigir a sala existente
        public string generaridsala()
        {
            string idsala= DateTime.Now.Year.ToString();
            var sala= (from s in milinq.Salas
                      where s.IdSala.StartsWith(idsala)
                      select s.IdSala).Max();
            if(sala.Length==0)
            {
                idsala = idsala + "001";
            }
            else
            {
                idsala = (Convert.ToInt32(sala) + 1).ToString();
            }
            return idsala;
        }
        public ModelMPrincipal crearnuevasala(ModelSalaNueva sala)
        {
            var verificasala = (from i in milinq.IntegrantesSalas
                               join s in milinq.Salas on i.IdSala equals s.IdSala
                               where s.IdTipoSala == 1 && i.IdPersona == sala.idpersonacreo
                               select new ModelMPrincipal
                               {
                                   idintengrante = i.IdIntegrante,
                                   id_sala = i.IdSala
                               }).ToList();
           
            bool band = false;
            string sal = "";
            int integ = -1;

            foreach (var item in verificasala)
            {
                var inte = (from id in milinq.IntegrantesSalas
                            where id.IdSala == item.id_sala && id.IdPersona == sala.idpersonaconta
                            select id).ToList();

                if (inte.Count() != 0)
                {
                    band = true;
                    sal = item.id_sala;
                    integ = item.idintengrante;
                    break;
                }
            }

            if (band)
            {
                ModelMPrincipal salaexistente= new ModelMPrincipal()
                {
                    idintengrante = integ,
                    id_sala = sal,
                };
                return salaexistente;
            }
            else
            {
                var nuevasala = new Sala
                {
                    IdSala = generaridsala(),
                    EstadoChat = true,
                    FechaIncio = DateTime.Now,
                    IdTipoSala = 1,
                };
                milinq.Salas.Add(nuevasala);
                milinq.SaveChanges();
                var nuevosintegrantes = new IntegrantesSala
                {
                    EstadoIntegrante=true,
                    EstadoAdministrador=false,
                    IdPersona=sala.idpersonacreo,
                    IdSala=nuevasala.IdSala
                };
                milinq.IntegrantesSalas.Add(nuevosintegrantes);
                milinq.SaveChanges();
                var nuevosintegrantes2 = new IntegrantesSala
                {
                    EstadoIntegrante = true,
                    EstadoAdministrador = false,
                    IdPersona = sala.idpersonaconta,
                    IdSala = nuevasala.IdSala
                };
                milinq.IntegrantesSalas.Add(nuevosintegrantes2);
                milinq.SaveChanges();

                ModelMPrincipal salanueva = new ModelMPrincipal()
                {
                    idintengrante = nuevosintegrantes.IdIntegrante,
                    id_sala = nuevasala.IdSala,
                };
                return salanueva;
            }
        }

        public ModelMPrincipal crearnuevasalagrupo(ModelSalanuevaGrupal sala)
        {

            var nuevasala = new Sala
            {
                IdSala = generaridsala(),
                EstadoChat = true,
                FechaIncio = DateTime.Now,
                IdTipoSala = 2,
                NombreSala=sala.NombreSala
            };
            milinq.Salas.Add(nuevasala);
            milinq.SaveChanges();
            var nuevosintegrantes = new IntegrantesSala
            {
                EstadoIntegrante = true,
                EstadoAdministrador = true,
                IdPersona = sala.idpersonacreo,
                IdSala = nuevasala.IdSala
            };
             milinq.IntegrantesSalas.Add(nuevosintegrantes);
            milinq.SaveChanges();
            foreach (var s in sala.idpersonaconta)
            {
                
                var nuevosintegrantes2 = new IntegrantesSala
                {
                    EstadoIntegrante = true,
                    EstadoAdministrador = false,
                    IdPersona = s.IdPersona,
                    IdSala = nuevasala.IdSala
                };
                milinq.IntegrantesSalas.Add(nuevosintegrantes2);
                milinq.SaveChanges();
            };

            //----------------------
            var nuevomensaje = new Mensaje
            {
                EstadoMensaje = true,
                FechaMensaje = DateTime.Now,
                Archivo = null,
                Imagen = null,
                Audio = null,
                Mensaje1 = "----Grupo creado---",
                IdIntegrante =nuevosintegrantes.IdIntegrante,
                IdSala = nuevasala.IdSala,
            };
            milinq.Mensajes.Add(nuevomensaje);
            milinq.SaveChanges();



            var nuevoestamen = new EstadoMensaje
            {
                NombreEstado = "sin leer",
                IdMensaje = nuevomensaje.IdMensaje,
                IdTipoSala =2
            };

            milinq.EstadoMensajes.Add(nuevoestamen);
            milinq.SaveChanges();
            //---------------------
            ModelMPrincipal salanueva = new ModelMPrincipal()
            {
                idintengrante = nuevosintegrantes.IdIntegrante,
                id_sala = nuevasala.IdSala,
            };
            return salanueva;
        }
    }
}
