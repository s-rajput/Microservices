export interface PetsData {
    Gender: string;
    Pets: CityPets[];
}

export interface CityPets {
    Name: string;
    City: string;
}

export interface PetsStatus {
    loaded: boolean,
    Pets: PetsData
}