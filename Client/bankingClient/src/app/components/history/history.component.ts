import { NumberInput } from '@angular/cdk/coercion';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Operation } from 'src/app/models/Operation';
import { HistoryService } from 'src/app/services/history.service';
import { operations } from 'src/app/Data/Dummy';
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
// pageIndex:NumberInput = 1;
// pageSize:NumberInput = 2;
// length:NumberInput = 4;
  constructor(private router: Router, private historyService: HistoryService) {
    const extras = this.router.getCurrentNavigation()?.extras;
    this.accountId = !!extras && !!extras.state ? extras.state['accountId'] : null;
    console.log(this.accountId);
    this.dataSource = new MatTableDataSource();
    this.dataSource.paginator = this.paginator;
    // this.paginator.pageIndex = 1;
    // this.paginator.pageSize = 100;
    // this.paginator.length = 100;
  
    this.getOperations()
    // this.paginator.length
  }
  ngOnInit(): void {
  }

  // public getOperations(pageIndex:NumberInput,pageSize:NumberInput): void {
  //   this.historyService.getOperationsByDetails(this.accountId, this.isChecked,pageIndex,pageSize).subscribe(op => {
  //     // this.paginator.pageIndex = pageIndex;
  //     // this.paginator.pageSize = pageSize;
  //     this.historyService.getNumberOperations(this.accountId).subscribe(
  //       num => this.paginator.length = num,err=> console.error(err)
  //     )
  //     this.dataSource = new MatTableDataSource(op);
  //     this.dataSource.paginator = this.paginator; 
  //   }, err => console.log(err))
  // }

  public getOperations():void{
    this.historyService.getOperationsByDetails(this.accountId, this.isChecked,1,2)
    .subscribe(res=>console.log(res),err=>console.log(err));
  }
  ngAfterViewInit(): void {
    // this.getOperations(1,2);
  }
  public changeChecked(): void {
    this.isChecked = !this.isChecked;
  }

}




// import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
// import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
// import { MatTableDataSource } from '@angular/material/table';
// import { Router } from '@angular/router';
// import { ForeignAccountDTO } from 'src/app/models/foreignAccountDetailsDTO.models';
// import { getOperationDTO } from 'src/app/models/getOperationDTO.models';
// import { OperationsHistoryDTO } from 'src/app/models/OperationsHistoryDTO.models';
// import { LoginService } from 'src/app/services/login.service';
// import { OperationsService } from 'src/app/services/operation.service';
// @Component({
//   selector: 'app-operations-history',
//   templateUrl: './operations-history.component.html',
//   styleUrls: ['./operations-history.component.css'],
// })
// export class OperationsHistoryComponent implements OnInit {
//   dataSource = new MatTableDataSource<OperationsHistoryDTO>();
//   displayedColumns: string[] = ['credit', 'accountID', 'amount', 'balance', 'date'];
//   currentAccountID: number = 0;
//   numOfOperaitons: number = 0;
//   foreignAccountDetails!: ForeignAccountDTO;
//   @ViewChild(MatPaginator) paginator!: MatPaginator;
//   constructor(private _loginService: LoginService, private _operationsService: OperationsService) { }
// ​
//   ngAfterViewInit() {
//     this.dataSource.paginator = this.paginator;
//     this.currentAccountID = this._loginService.getAccountID();
//     if (this.currentAccountID == 0) {
//       alert("faild to get your details");
//     }
//     this._operationsService.setnumberOperation(this.currentAccountID).subscribe(res => {
//       //intialize total number of operations-history
//       this.numOfOperaitons = res;
//     }, err =>  console.log(err));
// ​
//     this.getOperations();
//   }
//   ngOnInit(): void {
//     this.dataSource = new MatTableDataSource<OperationsHistoryDTO>();
//     //intialize
//     this.foreignAccountDetails = {
//       firstName: "sara",
//       lastName: "sara",
//       email: "sara@gmail.com",
//     }
//   }
//   //get foreign account details when expansing!
//   getforeignAccountDetails(accountID: number) {
//     this._loginService.GetForeignAccountDetails(accountID).subscribe((res) => {
//       this.foreignAccountDetails = res;
//     }, (err) => { console.log(err); });
//   }
// ​
//   getOperationsFromDB(getOperationDT0: getOperationDTO) {
//     this._operationsService.getOperation(getOperationDT0).subscribe(
//       (res) => {
//         this.dataSource.data = res;
//         this.paginator.length = this.numOfOperaitons;
//       }, (err) => {
//         alert("faild to get your operations")
//       }
//     );
//   }
//   //get operations in first time
//   getOperations() {
//     const getOperationDT0: getOperationDTO = {
//       currentAccountID: this.currentAccountID,
//       pageNumber: this.paginator.pageIndex,
//       numberOfRecords: this.paginator.pageSize,
//     }
//     this.getOperationsFromDB(getOperationDT0);
//   }
//   //each page event get operations
//   getNextPage(pageEvent: PageEvent) {
//     const getOperationDT0: getOperationDTO = {
//       currentAccountID: this.currentAccountID,
//       pageNumber: pageEvent.pageIndex,
//       numberOfRecords: pageEvent.pageSize,
//     }
//     this.getOperationsFromDB(getOperationDT0);
//   }
// }
