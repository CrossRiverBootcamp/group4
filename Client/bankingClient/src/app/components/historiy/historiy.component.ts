import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Operation } from 'src/app/models/Operation';
import { HistoryService } from 'src/app/services/history.service';

@Component({
  selector: 'app-historiy',
  templateUrl: './historiy.component.html',
  styleUrls: ['./historiy.component.css']
})
export class HistoriyComponent implements OnInit {
  displayedColumns = ['DebitOrCredit', 'FromTo', 'amount', 'balance','OperationDate'];
  dataSource!: MatTableDataSource<Operation>;
  constructor(private historyService : HistoryService ) {
    this.dataSource = new MatTableDataSource();
   }

  ngOnInit(): void {
    this.historyService.getAllOperations().subscribe(op=>{
      this.dataSource = new MatTableDataSource(op);
      },err=>console.log(err))
  }

}
