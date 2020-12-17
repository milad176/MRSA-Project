import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { HousingService } from 'src/app/services/housing.service';
import { ActivatedRoute } from '@angular/router';
import { IPropertyBase } from 'src/app/model/IPropertyBase';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css'],
})
export class PropertyListComponent implements OnInit {
  properties: IPropertyBase[];
  SellRent = 1;
  city = '';
  cityFilter = '';
  SortbyParam = '';
  SortDirection = 'asc';


  constructor(
    private housingService: HousingService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    if (this.route.snapshot.url.toString()) {
      this.SellRent = 2;
    }
    this.housingService.getAllProperties(this.SellRent).subscribe((data) => {
      this.properties = data;
      console.log(this.properties);
    });
  }

  onCityFilter(){
      this.cityFilter = this.city;
  }

  onCityFilterClear(){
    this.cityFilter = '';
    this.city = '';
  }

  onSortDirection(){
    if(this.SortDirection ==='asc'){
      this.SortDirection = 'desc';
    }else{
      this.SortDirection = 'asc';
    }
  }
}
