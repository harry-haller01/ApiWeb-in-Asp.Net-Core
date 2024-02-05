using Labiofam.Models;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de relaciones Producto/Punto de venta.
    /// </summary>
    public class ProductPOSService : RelationService<Product_POS>,
        IRelationService<Product_POS>,
        IProductPOSService
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public ProductPOSService(WebDbContext webDbContext)
            : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Agrega una nueva relación entre un producto y un punto de venta.
        /// </summary>
        /// <param name="product_id">ID del producto.</param>
        /// <param name="pos_id">ID del punto de venta.</param>
        public override async Task AddAsync(Guid product_id, Guid pos_id)
        {
            try
            {
                var current_product_POS = await GetAsync(product_id, pos_id);
                current_product_POS.Cantidad += 1;
            }
            catch
            {
                var relation = new Product_POS
                {
                    Id1 = product_id,
                    Id2 = pos_id,
                    Cantidad = 1
                };
                await _webDbContext.AddAsync(relation);
            }
            await _webDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método asincrónico para agregar una cantidad específica de un producto a un punto de venta.
        /// </summary>
        /// <param name="product_id">ID del producto.</param>
        /// <param name="pos_id">ID del punto de venta.</param>
        /// <param name="size">Tamaño/cantidad a agregar.</param>
        public async Task AddAsync(Guid product_id, Guid pos_id, int size)
        {
            try
            {
                var current_product_POS = await GetAsync(product_id, pos_id);
                current_product_POS.Cantidad += size;
            }
            catch
            {
                var relation = new Product_POS
                {
                    Id1 = product_id,
                    Id2 = pos_id,
                    Cantidad = size
                };
                await _webDbContext.AddAsync(relation);
            }
            await _webDbContext.SaveChangesAsync();
        }
    }
}