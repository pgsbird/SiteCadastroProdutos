using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SiteMercadoProdutos.Data;
using SiteMercadoProdutos.Dtos;
using SiteMercadoProdutos.Models;

namespace SiteMercadoProdutos.Controllers
{
    
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISiteMercadoRepo _repository;
        private readonly IMapper _mapper;

        public ProductsController(ISiteMercadoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var productsItems = _repository.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productsItems));
        }

        //api/products/{id}
        [HttpGet("{id}", Name="GetProductById")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            var productItem = _repository.GetProductById(id);
            if(productItem != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(productItem));
            }
            return NotFound();
        }

        //POST api/products
        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            
            var productModel = _mapper.Map<Product>(productCreateDto);
            ExtraiImagem(productModel);

            _repository.CreateProduct(productModel);
            _repository.SaveChanges();


            var productReadDto = _mapper.Map<ProductReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id }, productReadDto);
        }

        //PUT api/products/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var productModelFromRepo = _repository.GetProductById(id);
            if (productModelFromRepo == null){
                return NotFound();
            }
            if (productModelFromRepo.Image != productUpdateDto.Image)
                System.IO.File.Delete(productModelFromRepo.Image);
            
            _mapper.Map(productUpdateDto,productModelFromRepo);
            ExtraiImagem(productModelFromRepo);

            _repository.UpdateProduct(productModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
            
        }

        //DELETE api/products/{id}
        [HttpDelete("{id}")]
         public ActionResult ProductDelete(int id)
        {
            var productModelFromRepo = _repository.GetProductById(id);

            if (productModelFromRepo == null){
                return NotFound();
            }

            _repository.DeleteProduct(productModelFromRepo);
            _repository.SaveChanges();

            System.IO.File.Delete(productModelFromRepo.Image);
            
            return NoContent();

        }

        private static void ExtraiImagem(Product productModel)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(productModel.Image);
            var nomeFinal = Guid.NewGuid().ToString() + fileInfo.Extension;
            var arquivoFinal = Path.Combine(fileInfo.DirectoryName, nomeFinal).Replace("Temp", "Images");

            System.IO.File.Move(productModel.Image, arquivoFinal);
            productModel.Image = productModel.Image.Replace("Temp", "Images").Replace(fileInfo.Name, nomeFinal);
        }
        
    }
}