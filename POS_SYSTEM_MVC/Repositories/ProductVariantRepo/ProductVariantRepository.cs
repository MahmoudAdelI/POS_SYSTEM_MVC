using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.ProductVariantRepo
{
<<<<<<< HEAD
    public class ProductVariantRepository(POSContext context) 
        : BaseRepository<ProductVariant>(context), IProductVariantRepository
    {

=======
    public class ProductVariantRepository : BaseRepository<ProductVariant>, IProductVariantRepository
    {
        public ProductVariantRepository(POSContext context) : base(context)
        {
        }
>>>>>>> Discount_MOW
    }
}
