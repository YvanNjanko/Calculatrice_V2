using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Entrez une expression mathématique :");
        string expression = Console.ReadLine();

        double resultat = Calculate(expression);
        Console.WriteLine("Résultat : " + resultat);

        Console.ReadLine();
    }

    static double Calculate(string expression)
    {
        expression = expression.Replace(" ", ""); // Supprimer les espaces

        double resultat = 0;
        double nombreActuel = 0;
        double nombrePrecedent = 0;
        char operateurActuel = '+';
        bool estOperateurActuel = false;

        for (int i = 0; i < expression.Length; i++)
        {
            char caractere = expression[i];

            if (char.IsDigit(caractere)) // Vérifier si le caractère est un chiffre
            {
                nombreActuel = (nombreActuel * 10) + double.Parse(caractere.ToString());
                estOperateurActuel = false;
            }
            else if (caractere == '(') // Vérifier si le caractère est une parenthèse ouvrante
            {
                int parentheseFermanteIndex = TrouverParentheseFermanteIndex(expression.Substring(i));
                string sousExpression = expression.Substring(i + 1, parentheseFermanteIndex - 1);
                nombreActuel = Calculate(sousExpression);
                i += parentheseFermanteIndex; // Ignorer les caractères de la sous-expression traitée
                estOperateurActuel = false;
            }
            else if (estOperateurActuel || i == 0) // Vérifier si le caractère est un opérateur ou si c'est le premier caractère
            {
                operateurActuel = caractere;
                estOperateurActuel = true;
            }
            else // Le caractère est un opérande
            {
                nombrePrecedent = EffectuerOperation(nombrePrecedent, nombreActuel, operateurActuel);
                operateurActuel = caractere;
                nombreActuel = 0;
                estOperateurActuel = true;
            }
        }

        resultat = EffectuerOperation(nombrePrecedent, nombreActuel, operateurActuel);
        return resultat;
    }

    static int TrouverParentheseFermanteIndex(string expression)
    {
        int compteurParentheses = 1;

        for (int i = 1; i < expression.Length; i++)
        {
            if (expression[i] == '(')
                compteurParentheses++;
            else if (expression[i] == ')')
                compteurParentheses--;

            if (compteurParentheses == 0)
                return i;
        }

        throw new ArgumentException("Expression mathématique invalide : parenthèse fermante manquante.");
    }

    static double EffectuerOperation(double operand1, double operand2, char operateur)
    {
        switch (operateur)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                return operand1 / operand2;
            default:
                throw new ArgumentException("Opérateur invalide : " + operateur);
        }
    }
}