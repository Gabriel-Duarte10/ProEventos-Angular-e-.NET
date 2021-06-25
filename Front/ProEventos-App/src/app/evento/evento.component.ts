import { HttpClient, HttpClientModule } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-evento',
  templateUrl: './evento.component.html',
  styleUrls: ['./evento.component.scss']
})
export class EventoComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados : any = [];
  widthImagem = 150;
  marginImagem = 2;
  exibirImagem =  true;
  private _filterList: string = "";

  public get filterList(): string{
    return this._filterList;
  }
  public set filterList(value:string){
    this._filterList = value;
    this.eventosFiltrados = this.filterList   ? this.filterEventos(this.filterList) : this.eventos;
  }
  filterEventos(filterBy: string ): any{
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter( evento => evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  alterarImagem(){
    this.exibirImagem = !this.exibirImagem;
  }

  public getEventos(): void{
    this.http.get('https://localhost:5001/api/evento').subscribe(
      response =>{
        this.eventos = response,
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
      );
  }

}
