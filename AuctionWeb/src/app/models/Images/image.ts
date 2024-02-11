export interface Image {
    publicId: string | null | undefined;
    image: File | undefined;
    imageUrl: string | ArrayBuffer | null | undefined;
}