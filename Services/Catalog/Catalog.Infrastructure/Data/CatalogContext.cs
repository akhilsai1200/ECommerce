using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext
    {
        public IMongoCollection<Product> Products { get;}
        public IMongoCollection<ProductBrand> Brands { get;}
        public IMongoCollection<ProductType> Types { get;}
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var dataBase = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            var Brands = dataBase.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
            var Types = dataBase.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
            var Products = dataBase.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
