import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/brands';
import { IType } from '../shared/models/productType';



@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products:IProduct[];
  brands :IBrand[];
  types:IType[];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected ='name';
  sortOptions = [
    {name: 'Alphabetical',value:'name'},
    {name :'Price:Low to High', value:'priceAsc'},
    {name :'Price:High to low',value:'priceDesc'}  
  ]

  constructor(private shopService:ShopService ) 
  {

  }

  ngOnInit() 
  {
    this.getProducts();
    this.getBrands();
    this.getTypes();
 
  }


  getProducts() {
    this.shopService.getProducts(this.brandIdSelected
      , this.typeIdSelected
      ,this.sortSelected).subscribe(
      (response) => {
        this.products = response.data; // Assuming response contains a 'data' property
      },
      (error) => {
        console.log(error);
      }
    );
  }
  

getBrands() {
  this.shopService.getBrands().subscribe(
    (response: IBrand[]) => {
      // Assuming that your service returns an array of brand objects
      this.brands = [{ id: 0, name: 'All' }, ...response];
    },
    (error) => {
      console.log(error);
    }
  );
}


getTypes() {
  this.shopService.getTypes().subscribe(
    (response: IType[]) => {
      // Assuming that your service returns an array of product type objects
      this.types = [{ id: 0, name: 'All' }, ...response];
    },
    (error) => {
      console.log(error);
    }
  );
}

onBrandSelected(brandId:number)
{
  this.brandIdSelected =brandId;
  this.getProducts();
}
onTypeSelected(typeId:number)
{
  this.typeIdSelected = typeId;
  this.getProducts();
}

onSortSelected(event: any) {
  this.sortSelected = event.target.value;
  this.getProducts();
}

}
