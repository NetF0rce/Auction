import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuctionCreate } from '../../../models/auction/auction-create';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormDataService } from '../../../core/services/form.data.service';
import { ActivatedRoute } from '@angular/router';
import { AuctionDto } from '../../../models/auction/auction-dto';

@Component({
  selector: 'auction-create',
  templateUrl: './auction-create.component.html',
  styleUrl: './auction-create.component.css',
})
export class AuctionCreateComponent implements OnInit {
  auctionForm: FormGroup = new FormGroup({});
  imageUrls: File[] = [];
  id: string | null = '';
  constructor(
    private formBuilder: FormBuilder,
    private readonly httpClient: HttpClient,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.buildForm();
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id !== 'undefined') {
      this.httpClient.get<AuctionDto>(environment.apiUrl + 'auctions/' + this.id).subscribe((response: AuctionDto) => {
        this.auctionForm.patchValue(response);
      });
    }
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
      formData.imageUrls = this.imageUrls;
      const data = FormDataService.objectToFormData(formData);
      if (this.id !== 'undefined') {
        this.httpClient.put(environment.apiUrl + 'auctions/' + this.id, data).subscribe((response: any) => { });
      } else {
        this.httpClient.post(environment.apiUrl + 'auctions', data).subscribe((response: any) => { });
      }
    } else {
      // Handle form validation errors
    }
  }
}
