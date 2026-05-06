using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.Brands;

public class BrandRepository(POSContext context)
    : BaseRepository<Brand>(context), IBrandRepository
{
}