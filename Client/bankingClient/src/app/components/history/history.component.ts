import { NumberInput } from '@angular/cdk/coercion';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
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
export class historyComponent implements OnInit ,AfterViewInit{
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayedColumns = ['DebitOrCredit', 'FromTo', 'amount', 'balance', 'OperationDate'];
  dataSource!: MatTableDataSource<Operation>;
  accountId!: Number;
  isChecked: boolean = false;
pageIndex:NumberInput = 1;
pageSize:NumberInput = 2;
length:NumberInput = 4;
  constructor(private router: Router, private historyService: HistoryService) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
    this.dataSource = new MatTableDataSource();
    // this.paginator.pageIndex = this.pageIndex;
    // this.paginator.pageSize = this.pageSize;
    // this.paginator.length = this.length;
    this.dataSource.paginator = this.paginator;
    // this.paginator.length
  }
  ngOnInit(): void {
  }

  public getOperations(pageIndex:NumberInput,pageSize:NumberInput): void {
    this.historyService.getOperationsByDetails(this.accountId, this.isChecked,pageIndex,pageSize).subscribe(op => {
      this.paginator.pageIndex = pageIndex;
      this.paginator.pageSize = pageSize;
      this.historyService.getNumberOperations(this.accountId).subscribe(
        num => this.paginator.length = num,err=> console.error(err)
      )
      this.dataSource = new MatTableDataSource(op);
      this.dataSource.paginator = this.paginator; 
    }, err => console.log(err))
  }


  ngAfterViewInit(): void {
    this.getOperations(1,2);
  }
  public changeChecked(): void {
    this.isChecked = !this.isChecked;
  }

}
