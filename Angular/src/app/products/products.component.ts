
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product,ProductToCreateDto } from '@app/_models';
import { ProductService } from '@app/_services';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { environment } from '@environments/environment';
import { CurrencyMaskInputMode,CurrencyMaskDirective } from 'ngx-currency';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls:['./products.component.css']
})
export class ProductsComponent implements OnInit {
  public isCreate: boolean;
  public name: string;
  public value: string;
  public image: string;
  public product: ProductToCreateDto;
  public response: {dbPath:''}
  public productId: number;

  public ngxCurrencyOptions = {
      prefix: 'R$ ',
      thousands: '.',
      decimal: ',',
      allowNegative: false,
      nullable: true,
      max: 250_000_000,
      inputMode: CurrencyMaskInputMode.FINANCIAL,
    };

  constructor(private productService : ProductService, private router: Router,private route: ActivatedRoute ) { }

  ngOnInit() {
    this.isCreate = true;
    this.route.params.subscribe(params => this.productId = params['id']);
    if(this.productId){      
      this.productService.getProductById(this.productId).subscribe(res =>{
        this.product = res;
        this.name = res.name;
        this.image = res.image;
        this.value = res.value;
        this.isCreate = false;
      });
    }
      
  }

  public onCreate = () => {
    if (this.name && this.value && this.response ){
      this.product = {
            name: this.name,
            value: this.value,
            image: this.response.dbPath
          }
          this.productService.createProduct(this.product).subscribe(
            res => {
              confirm("Produto criado com sucesso!");
              this.router.navigate(['/']);
            });
    }else{
      confirm("Preencha os campos Nome, Valor e faça um upload antes de salvar.");
    }
   
  }

    public onUpdate = () => {
      if (this.response)
        this.image = this.response.dbPath;

    this.product = {
      name: this.name,
      value: this.value,
      image: this.image
    }     

    if (this.name && this.value){
      this.productService.updateProduct(this.productId,this.product).subscribe(
          res => {
            confirm("Produto atualizado com sucesso!");
            this.router.navigate(['/']);
          });
    }else{
      confirm("Preencha os campos Nome e Valor antes de salvar.");
    }
  
  }
  
  public returnToCreate = () => {
    this.isCreate = true;
    this.name = '';
    this.value = '';
    this.image = '';
  }

  public uploadFinished = (event) =>{
    this.response = event;
  }

  public createImgPath = (serverPath:string) =>{
    console.log(`${environment.servidorUrl}/${serverPath}`);
    return `${environment.servidorUrl}/${serverPath}`;
  }

}


