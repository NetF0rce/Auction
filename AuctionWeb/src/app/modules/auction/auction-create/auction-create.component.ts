import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuctionCreate } from '../../../models/Auction/auction-create';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'auction-info',
  templateUrl: './auction-info.component.html',
  styleUrl: './auction-create.component.css',
})
export class AuctionCreateComponent implements OnInit {
  auctionForm: FormGroup = new FormGroup({});
  imageUrls: File[] = [];
  constructor(private formBuilder: FormBuilder, private readonly httpClient: HttpClient) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(): void {
    this.auctionForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      imageUrls: [[]],
      startPrice: [0, Validators.required],
      finishIntervalTicks: [0, Validators.required]
    });
  }

  onFileChange(event: any): void {
    const files: FileList = event.target.files;
    for (let i = 0; i < files.length; i++) {
      this.imageUrls.push(files[i]);
    }
    this.auctionForm.patchValue({ imageUrls: this.imageUrls });
  }

  onSubmit(): void {
    if (this.auctionForm.valid) {
      const formData: AuctionCreate = this.auctionForm.value;
      console.log(formData);
      this.httpClient.post(`${environment.apiUrl}/auctions`, formData);
    } else {
      // Handle form validation errors
    }
  }
}
