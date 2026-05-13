using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.SubCategories;

public class SubCategoryRepository(POSContext context)
    : BaseRepository<SubCategory>(context), ISubCategoryRepository
{
}