using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;

namespace POS_SYSTEM_MVC.Repositories.CategoryRepo
{
    public class CategoryRepository(POSContext context)
        : BaseRepository<Category>(context), ICategoryRepository
    {
    }
}