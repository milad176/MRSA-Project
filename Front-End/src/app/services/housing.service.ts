import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Property } from '../model/property';

@Injectable({
  providedIn: 'root',
})
export class HousingService {
  constructor(private http: HttpClient) {}

  getAllCities(): Observable<string[]>{
    return this.http.get<string[]>("http://localhost:64565/api/cities");
  }
  getProperty(id: number) {
    return this.getAllProperties().pipe(
      map(propertiesArray => {
        // throw new Error('Some error');
        return propertiesArray.find(p => p.Id === id);
      })
    );
  }

  getAllProperties(sellRent?: number): Observable<Property[]> {
    return this.http.get('data/properties.json').pipe(
      map((data) => {
        const propertyArray: Array<Property> = [];
        const newLocalProperty = JSON.parse(localStorage.getItem("newProp"));

        if(localStorage){
          for (const id in newLocalProperty) {
            if(sellRent){
              if (newLocalProperty[id].SellRent === sellRent) {
                propertyArray.push(newLocalProperty[id]);
              }
            }else{
              propertyArray.push(newLocalProperty[id]);
            }
          }
        }

        for (const id in data) {
          if(sellRent){
            if (data[id].SellRent === sellRent) {
              propertyArray.push(data[id]);
            }
          }else{
            propertyArray.push(data[id]);
          }
        }
        return propertyArray;
      })
    );
  }

  addProperty(property: Property){
    let newProp=[];
    if(localStorage.getItem("newProp")){
      newProp = JSON.parse(localStorage.getItem("newProp"));
      newProp = [property, ...newProp];
    }else{
      newProp = [property];
    }

    localStorage.setItem("newProp", JSON.stringify(newProp));
  }

  newPropID(){
    if(localStorage.getItem('PID')){
      localStorage.setItem('PID', String(+localStorage.getItem('PID')+1));
      return +localStorage.getItem('PID');
    }else{
      localStorage.setItem('PID', '101');
      return 101;
    }
  }
}
