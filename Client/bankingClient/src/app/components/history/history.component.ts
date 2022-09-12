import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
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
  displayedColumns = ['DebitOrCredit', 'FromTo', 'amount', 'balance','OperationDate'];
  dataSource!: MatTableDataSource<Operation>;
  accountId!:Number;
  isChecked:boolean = false;
  constructor(private router: Router,private historyService : HistoryService ) {
    const extras  = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;  
    console.log(this.accountId);
    this.dataSource = new MatTableDataSource();
    this.dataSource.paginator = this.paginator;
   }
  ngOnInit(): void {
    this.historyService.getAllOperations(this.accountId,this.isChecked).subscribe(op=>{
      this.dataSource = new MatTableDataSource(op);
      },err=>console.log(err))
  }
  public changeChecked(): void{
    this.isChecked = !this.isChecked;
  }

}
