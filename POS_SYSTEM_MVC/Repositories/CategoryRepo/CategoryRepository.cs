using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.CategoryRepo
{
    public class CategoryRepository(POSContext context)
        : BaseRepository<Category>(context), ICategoryRepository
    {
    }
}

//Namespace "POS_SYSTEM_MVC.Services.Category" does not match folder structure,
////expected "POS_SYSTEM_MVC.Servicies.Category"