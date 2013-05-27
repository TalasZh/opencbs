Feature: Simple fixed installment schedule

Scenario: Generate fixed installment schedule
    Given the "IL Monthly Repayment - Declining rate" loan product
    When I create a loan with the attributes
        | Name          | Value      |
        | Installments  | 6          |
        | Amount        | 1000       |
        | Interest rate | 0.03       |
        | Grace period  | 0          |
        | Start date    | 01.01.2013 |
    Then the schedule is
        | Number | Expected Date | Expected Interest | Expected Principal | Expected Total | Olb  |
        | 1      | 01.02.2013    | 30                | 155                | 185            | 1000 |
        | 2      | 01.03.2013    | 25                | 160                | 185            | 845  |
        | 3      | 01.04.2013    | 21                | 163                | 184            | 685  |
        | 4      | 01.05.2013    | 16                | 169                | 185            | 522  |
        | 5      | 03.06.2013    | 11                | 173                | 184            | 353  |
        | 6      | 01.07.2013    | 05                | 180                | 185            | 180  |
