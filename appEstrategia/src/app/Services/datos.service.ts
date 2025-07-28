import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environments } from '../../environments/environments'
import { Estrategia } from '../Models/estrategia'

@Injectable({
  providedIn: 'root'
})
export class DatosService {

  constructor(private httClient: HttpClient) {

  }

  public secretKey: string = '81643C2C-43F3-40BE-AA05-06F553CAF728';
  public uri: string = 'https://localhost:7073/api/Estrategia/generarEstrategia';

  
  GenerarEstrategia(MaximaVuetlas: number, Usuario: string, IdPiloto: number) {
    const headers = new HttpHeaders({
      "X-API-KEY": this.secretKey
    });

    var url = `${this.uri}/${MaximaVuetlas}/${Usuario}/${IdPiloto}`

    return this.httClient.get<Estrategia[]>(url, { headers })
  }
}
