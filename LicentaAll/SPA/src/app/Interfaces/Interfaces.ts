export interface ISlide {
  imgSrc: string;
  state: string;
}

export interface IAditionalDetail extends IMaintainableEntity {
  name: string;
  description: string;
  id: number;
}

export interface IProduct extends IMaintainableEntity {
  name: string;
  description: string;
  quantity?: number;
  inStock: boolean;
  price: number;
  aditionalDetails: IAditionalDetail[];
  imageSrc: string;
  id: number;
  rating: number;
  total?: number;
}

export interface IReview extends IMaintainableEntity {
  id: number;
  user_id: string;
  user_nickname: string;
  text: string;
  rating: number;
}

export interface IMaintainableEntity {
  row_version:number;
}

export interface IProductInCart {
  id: number;
  quantity: number;
}