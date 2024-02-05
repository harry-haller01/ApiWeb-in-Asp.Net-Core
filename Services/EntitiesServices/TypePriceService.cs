using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Tipos y Precios de los Productos.
    /// </summary>
    public class TypePriceService : EntityNoDTOService<Type_Price>,
        IEntityService<Type_Price>, IEntityNoDTOService<Type_Price>
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public TypePriceService(WebDbContext webDbContext)
            : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Edita un Type_Price por su ID.
        /// </summary>
        /// <param name="type_price_id">ID del Type_Price a editar.</param>
        /// <param name="edited_tp">Type_Price editado.</param>
        public override async Task EditAsync(Guid type_price_id, Type_Price edited_tp)
        {
            var current_tp = await GetAsync(type_price_id);
            current_tp.Type = edited_tp.Type;
            current_tp.Price = edited_tp.Price;
            current_tp.Capacity = edited_tp.Capacity;

            _webDbContext.Entry(current_tp).State = EntityState.Modified;
            await _webDbContext.SaveChangesAsync();
        }
    }
}