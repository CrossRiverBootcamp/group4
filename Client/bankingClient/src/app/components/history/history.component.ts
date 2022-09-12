import { NumberInput } from '@angular/cdk/coercion';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Operation } from 'src/app/models/Operation';
import { HistoryService } from 'src/app/services/history.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class historyComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayedColumns = ['DebitOrCredit', 'FromTo', 'amount', 'balance', 'OperationDate'];
  dataSource!: MatTableDataSource<Operation>;
  accountId!: Number;
  isChecked: boolean = false;
pageIndex!: Number;
pageSize!: Number;
length!: Number;
// length!: NumberInput;
// pageIndex!: NumberInput;
// pageSize!: NumberInput;
// pageEvent!:PageEvent;
  // page?: Number;
  // pageSize?: Number;
  constructor(private router: Router, private historyService: HistoryService) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
    this.dataSource = new MatTableDataSource();
    this.dataSource.paginator = this.paginator;
  }

  // public getServerData(event?:PageEvent){
  //   this.historyService.getOperationsByDetails(event).subscribe(
  //     response =>{
  //         this.dataSource = response.data;
  //         this.pageIndex = response.pageIndex;
  //         this.pageSize = response.pageSize;
  //         this.length = response.length;
  //     },
  //     error =>{
  //       // handle error
  //     }
  //   );
  //   return event;
  // }

  public getOperations(): void {
    this.historyService.getOperationsByDetails(this.accountId, this.isChecked,this.pageIndex, this.pageSize).subscribe(op => {
      this.dataSource = new MatTableDataSource(op);
    }, err => console.log(err))
  }

  ngOnInit(): void {
    this.historyService.getNumberOperations(this.accountId).subscribe(
      num => this.length = num,err=> console.error(err)
    )
    this.pageIndex = 1;
    this.pageSize = 2;
    this.getOperations();
  }
  public changeChecked(): void {
    this.isChecked = !this.isChecked;
  }

}
