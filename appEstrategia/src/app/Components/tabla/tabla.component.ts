import { Component } from '@angular/core';
import { DatosService } from '../../Services/datos.service'
import { Estrategia } from '../../Models/estrategia';


@Component({
  selector: 'app-tabla',
  templateUrl: './tabla.component.html',
  styleUrl: './tabla.component.css'
})
export class TablaComponent {

  public Estrategias: Estrategia[] = [];

  public PilotoId: number = 0;
  public Usuario: string = "";
  public MaximasVueltas: number = 0;

  constructor(private DatosService: DatosService) {

  }

  GetEstrategias() {
    this.DatosService.GenerarEstrategia(this.MaximasVueltas, this.Usuario, this.PilotoId)
      .subscribe(data => {
        console.log(data)
        this.Estrategias = data;
      })
  }
}
