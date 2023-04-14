#include <iostream>
#include <string>
#include <vector>

using namespace std;

// Define the Card class
class Card {
public:
    string suit;
    string rank;
};

int main() {
    // Initialize the suits and ranks
    vector<string> suits = {"Clubs", "Diamonds", "Hearts", "Spades"};
    vector<string> ranks = {"Nine", "Ten", "Jack", "Queen", "King", "Ace"};

    // Initialize the deck
    vector<Card> deck;
    for (int i = 0; i < suits.size(); i++) {
        for (int j = 0; j < ranks.size(); j++) {
            Card card;
            card.suit = suits[i];
            card.rank = ranks[j];
            deck.push_back(card);
        }
    }

    // Print the deck
    for (int i = 0; i < deck.size(); i++) {
        cout << deck[i].rank << " of " << deck[i].suit << endl;
    }

    return 0;
}