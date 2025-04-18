import { useEffect, useState } from 'react';
import Swiper from 'react-native-deck-swiper';
import { View, StyleSheet } from 'react-native';
import { RecipeCard } from './RecipeCard';
import useDishesRepository from "../app/repositories/dishesRepository";

export const RecipeSwiper = () => {
    const [dishes, setDishes] = useState([]);
    const {getDishes} = useDishesRepository();

    useEffect(() => {
        const fetchDishes = async () => {
            try {
                const response = await getDishes();
                console.log(response);
                setDishes(response.dishes);
            } catch (error) {
                console.error(error);
            }
        };

        fetchDishes();
    }, []);

    return (
        <View style={styles.container}>
            {dishes.length > 0 && (
                <Swiper
                    cards={dishes}
                    renderCard={(recipe) => <RecipeCard recipe={recipe} />}
                    onSwiped={(cardIndex) => {
                        console.log('Swiped', cardIndex);
                    }}
                    onSwipedRight={(cardIndex) => {
                        console.log('Polubiono', dishes[cardIndex].name);
                    }}
                    onSwipedLeft={(cardIndex) => {
                        console.log('Odrzucono', dishes[cardIndex].name);
                    }}
                    cardIndex={0}
                    backgroundColor="transparent"
                    stackSize={3}
                />
            )}
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingTop: 60,
        paddingHorizontal: 16,
    },
});