import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormDataService } from '../../../core/services/form.data.service';
import { ActivatedRoute } from '@angular/router';
import { AuctionDto } from '../../../models/auction/auction-dto';
import { Image } from '../../../models/Images/image';
import { AuctionCreate } from '../../../models/Auction/auction-create';
import { AuctionService } from '../../../core/services/auction.service';

@Component({
  selector: 'auction-create',
  templateUrl: './auction-create.component.html',
  styleUrl: './auction-create.component.scss',
})
export class AuctionCreateComponent implements OnInit {
  auctionForm: FormGroup = this.buildForm();
  actionEditId: string | undefined;
  id: string | undefined | null;
  images: Image[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private auctionService: AuctionService) { }

  ngOnInit(): void {
    this.buildForm();
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.auctionService.getAuctionById(this.id).subscribe((response: AuctionDto) => {
        this.auctionForm.patchValue(response);
      });
    }
  }

  buildForm() {
    return this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      imageUrls: [[]],
      startPrice: [0, Validators.required],
      finishInterval: [0, Validators.required]
    });
  }

  handleFileInput(imageInput: any | null) {
    if (imageInput) {
      const reader = new FileReader();

      reader.onload = (e) => {
        const img = new Image();
        img.onload = () => {
          this.images.push({
            imageUrl: e.target?.result,
            image: imageInput
          });
        };
        img.src = URL.createObjectURL(imageInput);
      };
      reader.readAsDataURL(imageInput);
    }
  }

  onSubmit(): void {
    if (this.auctionForm.valid) {
      const formData: AuctionCreate = this.auctionForm.value;
      console.log(formData);
      const data = FormDataService.objectToFormData(formData);
      
        var images = this.images.map(im => im.image)
        data.delete("images");

        for (var i = 0; i < images.length; i++) {
          data.append('images', images[i]);
        


      if (this.id) {
        this.auctionService.editAuction(this.id, data).subscribe();
      }
      else {
        this.auctionService.createAuction(data).subscribe();
      }
    }
    else {
      // Handle form validation errors
    }
  }

  dropPhoto(index: number) {
    this.images.splice(index, 1);
  }
}
