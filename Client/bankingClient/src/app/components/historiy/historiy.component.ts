import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Operation } from 'src/app/models/Operation';

@Component({
  selector: 'app-historiy',
  templateUrl: './historiy.component.html',
  styleUrls: ['./historiy.component.css']
})
export class HistoriyComponent implements OnInit {
  displayedColumns = ['DebitOrCredit', 'FromTo', 'amount', 'balance','OperationDate'];
  dataSource!: MatTableDataSource<Operation>;
  constructor() { }

  ngOnInit(): void {
  }

}
