using AutoMapper;
using ShoppingCart.application.Dtos.Attribute;
using ShoppingCart.application.Dtos.GroupAttribute;
using ShoppingCart.application.Dtos.Product;
using ShoppingCart.domain.Entities;
using ShoppingCart.domain.Entities.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, ProductModel>().ReverseMap();
            CreateMap<AttributeDto, AttributeModel>().ReverseMap();
            CreateMap<GroupAttributeDto, GroupAttributeModel>().ReverseMap();

            CreateMap<GroupAttributeDto, GroupAttributeRequestDto>().ReverseMap();
            CreateMap<AttributeDto, AttributeRequestDto>().ReverseMap();
            CreateMap<ProductDto, ProductRequestDto>().ReverseMap();

            CreateMap<GroupAttributeModel, GroupAttributeRequestDto>().ReverseMap();
            CreateMap<AttributeModel, AttributeRequestDto>().ReverseMap();
            CreateMap<ProductModel, ProductRequestDto>().ReverseMap();

            CreateMap<ProductModel, CartProductDto>().ReverseMap();
            CreateMap<AttributeModel, CartAttributeDto>().ReverseMap();
            CreateMap<GroupAttributeModel, CartProductGroupDto>().ReverseMap();


            CreateMap<CartAttributeDto, GroupAttributeRequestDto>().ReverseMap();
            CreateMap<CartProductGroupDto, AttributeRequestDto>().ReverseMap();
            CreateMap<CartProductDto, ProductRequestDto>().ReverseMap();

            CreateMap<ProductDto, CartProductDto>().ReverseMap();
            CreateMap<AttributeDto, CartAttributeDto>().ReverseMap();
            CreateMap<GroupAttributeDto, CartProductGroupDto>().ReverseMap();

            CreateMap<CartDto, CartModel>().ReverseMap();
            CreateMap<CartProductDto, CartProductModel>().ReverseMap();
            CreateMap<CartProductGroupDto, CartProductGroupModel>().ReverseMap();
            CreateMap<CartAttributeDto, CartAttributeModel>().ReverseMap();
            CreateMap<ProductUpdatedDto, ProductRequestDto>().ReverseMap();

        }
    }
}
