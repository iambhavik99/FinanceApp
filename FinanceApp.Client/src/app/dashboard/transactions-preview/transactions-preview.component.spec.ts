import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionsPreviewComponent } from './transactions-preview.component';

describe('TransactionsPreviewComponent', () => {
  let component: TransactionsPreviewComponent;
  let fixture: ComponentFixture<TransactionsPreviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TransactionsPreviewComponent]
    });
    fixture = TestBed.createComponent(TransactionsPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
