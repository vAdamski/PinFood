import { View, Text, ImageBackground, StyleSheet, TouchableWithoutFeedback } from 'react-native';
import { useState } from 'react';

export const RecipeCard = ({ recipe }) => {
    const [imageIndex, setImageIndex] = useState(0);

    const handleImageTap = () => {
        setImageIndex((prevIndex) => (prevIndex + 1) % recipe.images.length);
    };

    return (
        <View style={styles.card}>
            <TouchableWithoutFeedback onPress={handleImageTap}>
                <ImageBackground source={{ uri: recipe.images[imageIndex] }} style={styles.imageBackground} imageStyle={{ borderRadius: 16 }}>
                    <View style={styles.overlay}>
                        <Text style={styles.title}>{recipe.name}</Text>
                        <Text style={styles.description}>{recipe.description}</Text>
                    </View>
                </ImageBackground>
            </TouchableWithoutFeedback>
        </View>
    );
};

const styles = StyleSheet.create({
    card: {
        backgroundColor: '#fff',
        borderRadius: 16,
        padding: 16,
        shadowColor: '#000',
        shadowOpacity: 0.1,
        shadowRadius: 10,
        elevation: 5,
        width: '100%',
        height: '95%',
    },
    imageBackground: {
        flex: 1,
        resizeMode: 'cover',
        justifyContent: 'flex-end',
    },
    overlay: {
        backgroundColor: 'rgba(0, 0, 0, 0.2)',
        padding: 16,
        borderBottomLeftRadius: 16,
        borderBottomRightRadius: 16,
    },
    title: {
        fontSize: 22,
        fontWeight: 'bold',
        color: '#fff',
    },
    description: {
        fontSize: 16,
        color: '#fff',
        marginTop: 4,
    },
});