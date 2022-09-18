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
export class historyComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayedColumns = ['DebitOrCredit', 'otherSide', 'amount', 'balance', 'operationTime'];
  dataSource!: MatTableDataSource<Operation>;
  accountId!: Number;
  length = 0;
  pageIndex = 1;
  pageSize = 2;
  isChecked: boolean = false;
  constructor(private router: Router, private historyService: HistoryService) {

    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
  }
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<Operation>();
    this.getLength();
    this.getOperations()
  }
  details(accountIdOtherSide: Number) {
    this.router.navigateByUrl('/details', { state: { accountId: accountIdOtherSide } });
  }
  public getOperations(): void {
    this.historyService.getOperationsByDetails(this.accountId, this.isChecked, this.pageIndex, this.pageSize)
      .subscribe(res => {
        console.log(res);
        this.dataSource.data = res;
        this.paginator.pageIndex = this.pageIndex;
        this.paginator.length = this.length;
      }
        , err => console.log(err));
  }
  ngAfterViewInit(): void {
    this.dataSource = new MatTableDataSource<Operation>();
  }
  public changeChecked(): void {
    this.isChecked = !this.isChecked;
    this.getOperations();
  }
  public getLength(): void {
    this.historyService.getNumberOfOperations(this.accountId).subscribe(
      res => { this.length = res },
      err => { console.log(err) }
    )
  }
  onPaginateChange(pageEvent: PageEvent) {
    this.pageIndex = pageEvent.pageIndex;
    this.pageSize = pageEvent.pageSize;
    this.getLength();
    this.getOperations();
  }

}