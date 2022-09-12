using Business.Abstract;
using DataAccess.Concrete.InMemory;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Entities.DTOs;
using Core.Utilities.Results;
using Core;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using DataAccess.Concrete.EntityFramework;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using Business.BusinessAspect.Autofac;

namespace Business.Concrete
{
    //business codes = iş kurallarına uygunluk // İş kurallarını method olarak ayır!!!
    //validation = doğrulama // Attribute

    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ProductValidator))]//AOP
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.ProductId),
                 CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new Result(true, Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 02)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.CategoryId == categoryId), Messages.ProductListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductId == productId), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.UnitPrice >= min && x.UnitPrice <= max), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductListed);
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(x => x.ProductName == productName).Any();
            if (!result)
            {
                return new SuccessResult(Messages.ProductAdded);
            }

            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll().Data.Count;

            if (result < 15)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CheckIfCategoryLimitExceded);
        }
    }
}
