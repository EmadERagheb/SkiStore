import { Address } from "../address";
import { OrderItem } from "./order-item";

export interface Order {
    id: number;
    buyerEmail: string;
    orderDate: string;
    shipToAddress: Address;
    deliveryMethod: string;
    shippingPrice: string;
    orderItems: OrderItem[];
    subtotal: number;
    status: string;
    total: number;
  
}


