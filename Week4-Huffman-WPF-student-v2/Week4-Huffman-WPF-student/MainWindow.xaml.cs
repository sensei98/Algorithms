using System.Windows;

namespace Huffman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Huffman huffman;

        public MainWindow()
        {
            InitializeComponent();

            this.huffman = new Huffman();
            btnDecode.IsEnabled = false;
        }

        private void btnEncode_Click(object sender, RoutedEventArgs e)
        {
            lstFrequencies.Items.Clear();

            string inputMessage = txtInputText.Text;
            string binaryCode = huffman.Encode(inputMessage, lstFrequencies);
            txtEncodedText.Text = binaryCode;

            int nrOfInputBits = inputMessage.Length * 8;
            int nrOfEncodedBits = binaryCode.Length;
            lblIputLength.Content = $"{nrOfInputBits} bits";
            lblEncodedLength.Content = $"{nrOfEncodedBits} bits";

            // show reduction
            if (nrOfInputBits > 0)
            {
                double reduction = ((double)nrOfInputBits - nrOfEncodedBits) / nrOfInputBits * 100;
                lblReduction.Content = $"{reduction:0.0}" + "%";
            }
            else
                lblReduction.Content = "";


            btnDecode.IsEnabled = true;
        }

        private void btnDecode_Click(object sender, RoutedEventArgs e)
        {
            string binaryCode = txtEncodedText.Text;
            string decodedMessage = huffman.Decode(binaryCode);
            txtDecodedText.Text = decodedMessage;
        }
    }
}
