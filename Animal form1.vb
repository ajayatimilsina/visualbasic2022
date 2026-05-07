Public Class Form1

    Private Sub ButtonAnimalInstanceCreate_Click(
        sender As Object,
        e As EventArgs
    ) Handles ButtonAnimalInstanceCreate.Click

        ' Animalクラス型のcat変数を宣言
        Dim cat As Animal

        ' catにAnimalクラスのインスタンス生成
        cat = New Animal()

        ' catインスタンスに具体的な値を設定
        cat.name = "ネコ"
        cat.color = "白"

        ' catインスタンスの値をテキストボックスに表示
        TextBoxName1.Text = cat.name
        TextBoxColor1.Text = cat.color
        TextBoxSing1.Text = cat.Sing()

        ' Animalクラス型のdog変数を宣言
        Dim dog As Animal

        ' dogにAnimalクラスのインスタンス生成
        dog = New Animal()

        ' dogインスタンスに具体的な値を設定
        dog.name = "イヌ"
        dog.color = "茶"

        ' dogインスタンスの値をテキストボックスに表示
        TextBoxName2.Text = dog.name
        TextBoxColor2.Text = dog.color
        TextBoxSing2.Text = dog.Sing()

    End Sub

End Class
