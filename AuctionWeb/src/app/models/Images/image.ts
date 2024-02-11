export interface Image {
    publicId: string | null | undefined;
    image: File | undefined;
    id: string | undefined | null;
    imageUrl: string | ArrayBuffer | null | undefined;
}
