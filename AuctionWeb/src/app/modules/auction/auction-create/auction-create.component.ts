import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormDataService } from '../../../core/services/form.data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Image } from '../../../models/Images/image';
import { AuctionCreate } from '../../../models/Auction/auction-create';
import { AuctionService } from '../../../core/services/auction.service';
import { ToastrService } from 'ngx-toastr';
import { AuctionDto } from '../../../models/auction/auction-dto';

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
  oldImages: string[] = []

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private auctionService: AuctionService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.buildForm();
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.auctionService.getAuctionById(this.id).subscribe((response: AuctionDto) => {
        this.auctionForm.patchValue(response);
        this.images = response.images;
        this.oldImages = this.images.map(im => im.id as string);
      });
    }
  }

  buildForm() {
    return this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      startPrice: [null, [Validators.required, Validators.min(0)]],
      finishInterval: [null, [Validators.required, Validators.min(0)]]
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
          } as Image);
        };
        img.src = URL.createObjectURL(imageInput);
      };
      reader.readAsDataURL(imageInput);
    }
  }

  onSubmit(): void {
    if (this.auctionForm.valid) {
      const formData: AuctionCreate = this.auctionForm.value;


      const data = FormDataService.objectToFormData(formData);
      var images = this.images.filter(image => !image.id).map(im => im.image)
      data.delete("images");

      for (var i = 0; i < this.images.length; i++) {

        if (this.images[i].publicId) {
          const id = this.images[i].publicId;
          if (id !== undefined && id !== null)
            data.append(`oldPhotos`, id);
        }

        const image = this.images[i].image
        if (image !== undefined)
          data.append('images', image);
      }

      if (this.id) {
        this.auctionService.editAuction(this.id, data).subscribe(
          {
            next: val => this.router.navigate(["/auctions"])
          }
        );
      }
      else {
        if (this.images.length == 0) {
          this.toastr.error("You have to insert photo")
        }

        this.auctionService.createAuction(data).subscribe(
          {
            next: val => this.router.navigate(["/auctions"])
          }
        );
      }
    }
  }


  dropPhoto(index: number) {

    if (this.images[index].id) {
      this.oldImages = this.oldImages.filter(id => id != this.images[index].id);
    }

    this.images.splice(index, 1);
  }
}
