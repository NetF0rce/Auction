export interface Image {
    id: string | undefined | null;
    image: File;
    imageUrl: string | ArrayBuffer | null | undefined;
}