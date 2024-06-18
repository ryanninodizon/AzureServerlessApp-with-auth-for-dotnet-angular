import {Injectable} from '@angular/core';
import {HousingLocation} from './housinglocation';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class HousingService {
  
  constructor(private http: HttpClient){}
  //url = 'http://localhost:3000/locations';
  //url = 'http://localhost:7071/api/items';
  url = 'https://apim-abp-conference.azure-api.net/api/items';

  async getAllHousingLocations(): Promise<HousingLocation[]> {
      try {
          const data = await this.http.get<HousingLocation[]>(this.url).toPromise();
          return data ?? [];
      } catch (error) {
          console.error('Error:', error);
          return [];
      }
  }
  async getHousingLocationById(id: number): Promise<HousingLocation | undefined> {
    try {
      return await this.http.get<HousingLocation>(`${this.url}/${id}`).toPromise();
     
    } catch (error) {
        console.error('Error:', error);
        return undefined;
    }
  }
  submitApplication(firstName: string, lastName: string, email: string) {
    console.log(firstName, lastName, email);
  }
  submitHouse(houseName: string, address: string,id: string) {

    const randomNumber = Math.floor(Math.random() * (200 - 80 + 1)) + 80;
    return this.http.post<HousingLocation>(`${this.url}/${id}`, {
              "id": id,
              "name": houseName,
              "city": address,
              "state": "CA",
              "photo": "https://picsum.photos/id/"+randomNumber+"/1200/800",
              "availableUnits": 0,
              "wifi": false,
              "laundry": true
          }).subscribe(
              (response) => {
                  // Handle successful response
                  console.log('POST request successful', response);
              },
              (error) => {
                  // Handle error
                  console.error('POST request error', error);
              }
          );
  }
  deleteHouse(id: any) {   
     this.http.delete(`${this.url}/${id}`).subscribe();    
  }
}
