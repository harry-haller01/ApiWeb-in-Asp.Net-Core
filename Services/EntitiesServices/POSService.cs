using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Puntos de venta.
    /// </summary>
    public class POSService : EntityNoDTOService<Point_of_Sales>,
        IEntityService<Point_of_Sales>, IEntityNoDTOService<Point_of_Sales>
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public POSService(WebDbContext webDbContext)
            : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Edita un punto de venta por su ID.
        /// </summary>
        /// <param name="point_id">ID del punto de venta a editar.</param>
        /// <param name="edited_pos">Punto de venta editado.</param>
        public override async Task EditAsync(Guid point_id, Point_of_Sales edited_pos)
        {
            var current_pos = await GetAsync(point_id);
            current_pos.Name = edited_pos.Name;
            current_pos.Address = edited_pos.Address;
            current_pos.Image = edited_pos.Image;
            current_pos.Latitude = edited_pos.Latitude;
            current_pos.Longitude = edited_pos.Longitude;
            current_pos.Municipality = edited_pos.Municipality;
            current_pos.Province = edited_pos.Province;
            _webDbContext.Entry(current_pos).State = EntityState.Modified;
            await _webDbContext.SaveChangesAsync();
        }
    }
}