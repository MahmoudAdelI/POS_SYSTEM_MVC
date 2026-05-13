using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.SalesRepo
{
    public class SaleRepository(POSContext context) : BaseRepository<Sale>(context), ISaleRepository
    {
    }
}
