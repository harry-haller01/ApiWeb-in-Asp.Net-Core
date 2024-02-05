using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Servicios.
    /// </summary>
    public class ServiceService : EntityNoDTOService<Service>,
        IEntityService<Service>, IEntityNoDTOService<Service>
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public ServiceService(WebDbContext webDbContext)
            : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Edita un servicio por su identificador único.
        /// </summary>
        /// <param name="service_id">Identificador único del servicio a editar.</param>
        /// <param name="edited_service">El servicio editado.</param>
        public override async Task EditAsync(Guid service_id, Service edited_service)
        {
            var current_service = await GetAsync(service_id);
            current_service.Name = edited_service.Name;
            current_service.Info = edited_service.Info;
            _webDbContext.Entry(current_service).State = EntityState.Modified;
            await _webDbContext.SaveChangesAsync();
        }
    }
}