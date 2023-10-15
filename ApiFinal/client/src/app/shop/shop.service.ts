import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from '../shared/models/brands';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class ShopService 
{
baseurl ='https://localhost:7131/api/';
constructor(private http: HttpClient) { }

  getProducts(
      brandId?:number
      ,typeId?:number
      ,sort?:string
    )
  {
    let params = new HttpParams();
    debugger;

  if(brandId)
  {
   params = params.append('brandId',brandId.toString())
  }

  if(typeId)
  {
    params =  params.append('typeId',typeId.toString())

  }

  if(sort)
  {
    params = params.append('sort',sort);
  }
     return this.http.get<IPagination>(this.baseurl +'products',{observe:'response',params})
    .pipe(
      map(response =>
        {
          return  response.body;      
        })
    )
  }
  getBrands()
  {
    return this.http.get<IBrand[]>(this.baseurl + 'products/brands')
  }

  getTypes()
  {
    return this.http.get<IType[]>(this.baseurl + 'products/types')
  }

}

