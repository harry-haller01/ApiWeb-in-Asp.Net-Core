using Labiofam.Models;
using Microsoft.EntityFrameworkCore;

namespace Labiofam.Services
{
    /// <summary>
    /// Servicio de Productos.
    /// </summary>
    public class ProductService : EntityDTOService<Product, ProductDTO>,
        IEntityService<Product>, IEntityDTOService<Product, ProductDTO>
    {
        private readonly WebDbContext _webDbContext;

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        /// <param name="webDbContext">Contexto de la base de datos.</param>
        public ProductService(WebDbContext webDbContext)
            : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        /// <param name="new_model">Modelo del producto a agregar.</param>
        /// <returns>El producto ya agregado.</returns>
        public override async Task<Product> AddAsync(ProductDTO new_model)
        {
            try
            {
                await GetAsync(new_model.Product!.Name!);
                throw new InvalidOperationException("The product name is already taken");
            }
            catch
            {
                var result = new Product()
                {
                    Name = new_model.Product!.Name,
                    Type_of_Product = new_model.Product.Type_of_Product,
                    Description = new_model.Product.Description,
                    Image = new_model.Product.Image,
                    Advantages = new_model.Product.Advantages,
                    Diseases = new_model.Product.Diseases,
                    Summary = new_model.Product.Summary
                };
                await _webDbContext.AddAsync(result);

                foreach (var type in new_model.Types!)
                {
                    await _webDbContext.AddAsync(type);
                    await _webDbContext.AddAsync(new Type_Product()
                    {
                        Id1 = type.Id,
                        Id2 = result.Id
                    });
                }

                await _webDbContext.SaveChangesAsync();

                return result;
            }
        }

        /// <summary>
        /// Agrega una nueva colección de productos.
        /// </summary>
        /// <param name="new_models">Modelos de los productos a agregar.</param>
        /// <returns>Colección de los productos ya agregados.</returns>
        public override async Task<ICollection<Product>> AddAsync(ICollection<ProductDTO> new_models)
        {
            var products = new List<Product>();
            foreach (var product in new_models)
            {
                await AddAsync(product);
                products.Add(product.Product!);
            }

            return products;
        }

        /// <summary>
        /// Edita un producto por su ID.
        /// </summary>
        /// <param name="product_id">ID del producto a editar.</param>
        /// <param name="edited_Product">Producto editado.</param>
        public override async Task EditAsync(Guid product_id, ProductDTO edited_Product)
        {
            try
            {
                await GetAsync(edited_Product.Product!.Name!);
                throw new InvalidOperationException("The product name is already taken");
            }
            catch
            {
                var current_product = await GetAsync(product_id);
                current_product.Name = edited_Product.Product!.Name;
                current_product.Type_of_Product = edited_Product.Product.Type_of_Product;
                current_product.Description = edited_Product.Product.Description;
                current_product.Image = edited_Product.Product.Image;
                current_product.Advantages = edited_Product.Product.Advantages;
                current_product.Diseases = edited_Product.Product.Diseases;
                current_product.Summary = edited_Product.Product.Summary;

                var relations = await _webDbContext.Set<Type_Product>().ToListAsync();
                var types = await _webDbContext.Set<Type_Price>().ToListAsync();
                foreach (var item in relations)
                {
                    if (item.Id2.Equals(current_product.Id))
                    {
                        foreach (var type in types)
                        {
                            if (type.Id.Equals(item.Id1))
                                _webDbContext.Remove(type);
                        }
                        _webDbContext.Remove(item);
                    }
                }

                foreach (var type in edited_Product.Types!)
                {
                    await _webDbContext.AddAsync(type);
                    await _webDbContext.AddAsync(new Type_Product()
                    {
                        Id1 = type.Id,
                        Id2 = current_product.Id
                    });
                }

                _webDbContext.Entry(current_product).State = EntityState.Modified;
                await _webDbContext.SaveChangesAsync();
            }
        }
    }
}