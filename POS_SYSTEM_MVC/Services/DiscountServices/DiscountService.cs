using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.DiscountRepo;
using POS_SYSTEM_MVC.UnitOfWork;
using POS_SYSTEM_MVC.ViewModels;
using static POS_SYSTEM_MVC.Constants.Enums;

namespace POS_SYSTEM_MVC.Services.DiscountServices
{
    //MOW--start
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Discount MapToDiscount(DiscountVM model)
        {
            var discount = new Discount
            {
                Name = model.Name,
                Type = model.Type,
                Value = model.DiscountValue,
                CreatedAt = model.StartDate,
                ExpiresAt = model.EndDate,
                SaleTotalThreshold = model.SaleTotalThreshold,
                ProductId = model.ProductId,
                ProductVariantId = model.ProductVariantId,
                IsActive = model.IsActive
            };
            return discount;
        }
        public List<string> ValidateDiscount(DiscountVM model)
        {
            List<string> errors = [];

            if (model.Type == DiscountTypeENUM.Percentage
                && model.DiscountValue > 100)
            {
                errors.Add(
                    "Percentage can't exceed 100");
            }

            if (model.Type == DiscountTypeENUM.Fixed
                && model.DiscountValue <= 0)
            {
                errors.Add(
                    "Invalid fixed amount");
            }

            if (model.ProductId == null
                && model.ProductVariantId == null
                && model.SaleTotalThreshold == null)
            {
                errors.Add(
                    "Discount target is required");
            }

            if (model.EndDate != null
                && model.EndDate < model.StartDate)
            {
                errors.Add(
                    "End date must be greater than start date");
            }

            return errors;
        }

        //public void ValidateDiscount(DiscountVM model)
        //{
        //    if (model.Type == DiscountTypeENUM.Percentage && model.DiscountValue > 100)
        //    {
        //        throw new Exception("Percentage can't exceed 100");
        //    }
        //    if (model.Type == DiscountTypeENUM.Fixed && model.DiscountValue <= 0)
        //    {
        //        throw new Exception("Invalid fixed amount");
        //    }
        //    if (model.ProductId == null && model.ProductVariantId == null && model.SaleTotalThreshold == null)
        //    {
        //        throw new Exception(
        //            "Discount target is required");
        //    }
        //    if (model.EndDate != null && model.EndDate < model.StartDate)
        //    {
        //        throw new Exception(
        //            "End date invalid");
        //    }
        //}

        //public async Task CreateAsync(DiscountVM model)
        //{


        //    ValidateDiscount(model);

        //    var discount = MapToDiscount(model);

        //    await _unitOfWork
        //        .Discounts
        //        .AddAsync(discount);

        //    await _unitOfWork
        //        .SaveChangesAsync();
        //}
        public async Task<List<string>> CreateAsync(DiscountVM model)
        {
            var errors = ValidateDiscount(model);

            if (errors.Any())
            {
                return errors;
            }

            var discount = MapToDiscount(model);

            await _unitOfWork
                .Discounts
                .AddAsync(discount);

            await _unitOfWork
                .SaveChangesAsync();

            return [];
        }

        public async Task<IReadOnlyList<Discount>> GetAllAsync()
        {
            return await _unitOfWork.Discounts.GetAllAsync();
        }
        //MOW--end

    }
}
