using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using System.Linq.Expressions;

namespace POS_SYSTEM_MVC.Repositories.DiscountRepo
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(POSContext context) : base(context)
        {

        }
    }


}
