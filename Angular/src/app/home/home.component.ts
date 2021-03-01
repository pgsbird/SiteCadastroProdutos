import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { Product } from '@app/_models';
import { ProductService } from '@app/_services';
import { environment } from '@environments/environment';
import { Router } from '@angular/router';

@Component({ templateUrl: 'home.component.html', 
styleUrls:['./home.component.css'] })
export class HomeComponent {
    loading = false;
    products: Product[];

    constructor(private productService:ProductService,
        private router: Router,) { }

    ngOnInit() {
        this.listaProdutos();
    }

    private listaProdutos(){
        this.loading = true;
        
        this.productService.getAll().pipe(first()).subscribe(products => {
            this.loading = false;
            console.log(products);
            this.products = products;
        });
    }
    public createImgPath = (serverPath:string) =>{
        return `${environment.servidorUrl}/${serverPath}`;
      }

      public desejaDeletar = (product:Product) => {
        if(confirm("Deseja realmente deletar o produto:  '"+ product.name + "'")) {
            
            this.productService.removeProduct(product.id).subscribe(
                res => {
                    this.listaProdutos();
                    confirm("Produto removido com sucesso!");
                }
            )
          }
      }

      public FormataValor = (valor:string) =>
      {
         return  Number(valor).toFixed(2).toString().replace(".",",")
      }
}