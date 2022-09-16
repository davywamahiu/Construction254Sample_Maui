using Construction_Ke.Model;
using Construction_Ke.ViewModel;

using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;

using MySql.Data.MySqlClient;
using System.Data;

namespace Construction_Ke.Views.WeightBridge;

public partial class PayWB : CommunityToolkit.Maui.Views.Popup
{
    ListWeightViewModel ListWeightView = new();
    public PayWB()
	{
		InitializeComponent();
        BindingContext = new ListWeightViewModel();
        Size = new(900.0, 590.5);
        CanBeDismissedByTappingOutsideOfPopup = false;
        saved.IsEnabled = false;
        
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var previous = e.PreviousSelection;
        var current = e.CurrentSelection;
        //string tikiti = e.
        try
        {
            if(current != null)
                UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
            saved.IsEnabled = true;

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Item Slection Error", ex.Message, "OK");
        }
        //await Shell.Current.DisplayAlert("Test", "yeah", "OK");
    }
    DataTable dt1 = new();
    FinalReading finalReading;
    MySqlConnection mcon;
    string connString = "server=localhost;uid=root;pwd=;database=roben;";
    private async void UpdateSelectionData(IReadOnlyList<object> previousSelection, IReadOnlyList<object> currentSelection)
    {
        var selectedContact = currentSelection.FirstOrDefault() as FinalReading;
        try
        {
            if(selectedContact != null)
            {
                finalReading = new()
                {
                    TotalAmount = selectedContact.TotalAmount,
                    Balanc = selectedContact.Balanc,
                    InBank = selectedContact.InBank,
                    DateTime = selectedContact.DateTime,
                    Driver = selectedContact.Driver,
                    GrossWeight = selectedContact.GrossWeight,
                    Material = selectedContact.Material,
                    Tonage = selectedContact.Tonage,
                    Phone = selectedContact.Phone,
                    TareWeight = selectedContact.TareWeight,
                    Ticket = selectedContact.Ticket,
                    TonageRate = selectedContact.TonageRate,
                    Plate = selectedContact.Plate,
                    Time = selectedContact.Time
                };
                selectedContact.Balanc = selectedContact.InBank - selectedContact.TotalAmount;
                if (selectedContact.Balanc < 0)
                {
                    await Shell.Current.DisplayAlert("Customer Alert!!!", "Customer Balance is too low, Deficit is: KSh " + selectedContact.Balanc, "Continue");
                    return;
                }
                else
                    await Shell.Current.DisplayAlert("Customer Alert!!!", "Customer Balance is enough, Balance is: KSh " + selectedContact.Balanc + " Proceed", "Continue");

            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Test", ex.Message, "OK");
        }
        
    }

    private async void saved_Clicked(object sender, EventArgs e)
    {
        if (finalReading == null)
            return;
        if(!string.IsNullOrEmpty(finalReading.Ticket.ToString()))
        {
            mcon = new(connString);
            mcon.Open();
            indicato.IsVisible = true;
            // animate to 75% progress over 500 milliseconds with linear easing
            await indicato.ProgressTo(0.45, 1500, Easing.Linear);
            await Shell.Current.DisplayAlert("Please Wait.", "Your Receipt is being generated.", "OK");
            double spentAmount = 0;
            string cmdText2 = "Select * from wbcustomerdeposit WHERE plate='"+ finalReading.Plate+"'";
            try
            {
                MySqlCommand cmd2 = new(cmdText2, mcon);
                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = cmd2;
                adapter.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    spentAmount = Convert.ToDouble(dt1.Rows[i]["SpentAmount"].ToString());
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Number Plate Test", ex.Message, "OK");
            }
            spentAmount += finalReading.TotalAmount;
            finalReading.Balanc = finalReading.InBank - finalReading.TotalAmount;

            string cmdText = "INSERT INTO weighbridgesales (TotalAmount, Balance, InBank," + "DateTime," +
                "GrossWeight, Driva, Material, Tonage, Phone, TareWeight, Ticket,TonageRate" +
                ",Plate) " +
                "VALUES (@TotalAmount, @Balanc, @InBank,@DateTime,@GrossWeight,@Driver," +
                "@Material,@Tonage, @Phone, @TareWeight, @Ticket, @TonageRate, @Plate)";
            MySqlCommand cmd = new(cmdText, mcon);
            //DbInsert insert = new();
            cmd.Parameters.AddWithValue("@TotalAmount", finalReading.TotalAmount);
            cmd.Parameters.AddWithValue("@Balanc", finalReading.Balanc);
            cmd.Parameters.AddWithValue("@InBank", finalReading.InBank);
            cmd.Parameters.AddWithValue("@DateTime", finalReading.DateTime);
            cmd.Parameters.AddWithValue("@GrossWeight", finalReading.GrossWeight);
            cmd.Parameters.AddWithValue("@Driver", finalReading.Driver);
            cmd.Parameters.AddWithValue("@Material", finalReading.Material);
            cmd.Parameters.AddWithValue("@Tonage", finalReading.Tonage);
            cmd.Parameters.AddWithValue("@Phone", finalReading.Phone);
            cmd.Parameters.AddWithValue("@TareWeight", finalReading.TareWeight);
            cmd.Parameters.AddWithValue("@Ticket", finalReading.Ticket);
            cmd.Parameters.AddWithValue("@TonageRate", finalReading.TonageRate);
            cmd.Parameters.AddWithValue("@Plate", finalReading.Plate);
            cmd.ExecuteNonQuery();
            //
            
            string updte = "UPDATE wbcustomerdeposit SET Deposit='" + finalReading.Balanc + "',Balance='" + finalReading.Balanc + "', SpentAmount='"+spentAmount+"' where plate='"+ finalReading.Plate + "'";
            MySqlCommand cmd1 = new(updte, mcon);
            cmd1.ExecuteNonQuery();
            mcon.Close();
            
            try
            {
                PdfWriter writer = new("E:\\"+ finalReading.Ticket+ "_"+ finalReading.Plate + "_"+ finalReading.Driver + ".pdf");
                PdfDocument pdf = new(writer);
                Document document = new(pdf);
                Paragraph newline = new Paragraph(new Text("\n"));
                //Paragraph header = new Paragraph("CONSTRUCTION KE \n" +
                //    "Your Trusted Costruction Software Assisant")
                //   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                //   .SetBold()
                //   .SetUnderline()
                //   .SetFontSize(20);

                //document.Add(header);
                //header table
                Table tableHead = new Table(3);
                Microsoft.Maui.Controls.Image image = new Microsoft.Maui.Controls.Image { Source = "business_report.png" };
                tableHead.UseAllAvailableWidth();
                iText.Layout.Element.Cell cellimage = new iText.Layout.Element.Cell()
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .Add(new Paragraph("Logo.PNG"))
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT)
                    .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
                    .SetMarginBottom(1f);
                tableHead.AddCell(cellimage);
                //cellimage.AddElement(logo);

                Table inrtbl = new Table(1);
                Paragraph para = new Paragraph("CONSTRUCTION KE");
                iText.Layout.Element.Cell cellpara = new iText.Layout.Element.Cell()
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetFontSize(20).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                    .Add(para);
                inrtbl.AddCell(cellpara);
                Paragraph para2 = new Paragraph("Your Trusted Costruction Software Assisant");
                iText.Layout.Element.Cell cellpara2 = new iText.Layout.Element.Cell()
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetFontSize(15)
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                    .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
                    .Add(para2);
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph("Sales Receipt");
                iText.Layout.Element.Cell cellpara3 = new iText.Layout.Element.Cell()
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .Add(para3).SetFontSize(15)
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                    .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                inrtbl.AddCell(cellpara3);

                iText.Layout.Element.Cell cellParagraph = new iText.Layout.Element.Cell()
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT)
                    .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
                    .Add(inrtbl)
                    .SetMarginBottom(1f);
                tableHead.AddCell(cellParagraph);
                //Address cell
                Table tableAd = new(1);
                iText.Layout.Element.Cell address = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                   .SetFontSize(7).SetMinHeight(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                   .Add(new Paragraph("Phone: "+ finalReading.Phone + " \n" +
                   "P.O.Box 347676, \n" +
                   "Nyahururu, Kenya."));
                tableAd.AddCell(address);
                iText.Layout.Element.Cell address1 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                   .SetFontSize(7).SetMinHeight(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                   .Add(new Paragraph(" Email : constructio@example.com"));
                tableAd.AddCell(address1);
                iText.Layout.Element.Cell address2 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                   .SetFontSize(7).SetMinHeight(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                   .Add(new Paragraph(" Website : https://www/example.com"));
                tableAd.AddCell(address2);
                iText.Layout.Element.Cell address3 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                   .SetFontSize(7).SetMinHeight(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                   .Add(new Paragraph(" Dates : " + (finalReading.DateTime).ToString()));
                tableAd.AddCell(address3);
                iText.Layout.Element.Cell addresshead = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                   .SetFontSize(7).SetMinHeight(10).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                   .Add(tableAd);
                tableHead.AddCell(addresshead);
                //end address cell

                document.Add(tableHead);
                //end header table
                //Table table2 = new(3, false);

                //Paragraph subheader = new Paragraph("SALES RECEIPT")
                //.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                //.SetFontSize(15);
                //document.Add(subheader);
                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);
                document.Add(newline);
                Table table = new Table(2, false);
                
                //First Row
                iText.Layout.Element.Cell cell11 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(255).SetFontSize(14).SetMinHeight(15)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph(" Weight Id: "+ finalReading.Code));
                iText.Layout.Element.Cell cell12 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(255)
                   .SetFontSize(14).SetMinHeight(15)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph(" Ticket No: " + finalReading.Ticket));
                //End Row 1
                //Pending row 2
                iText.Layout.Element.Cell cell21 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" Agent Name: David"));
                iText.Layout.Element.Cell cell22 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" Dates : "+ (finalReading.DateTime).ToString()));
                //end row 2
                //begin row 3
                iText.Layout.Element.Cell cell31 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph("Customer Name: "+ finalReading.Driver));
                iText.Layout.Element.Cell cell32 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph("Contact: " + finalReading.Phone));
                //end row 3
                //begin row 4
                iText.Layout.Element.Cell cell41 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" No. Plate: " + finalReading.Plate));
                iText.Layout.Element.Cell cell42 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" Weight: " + finalReading.GrossWeight));
                //end row 4
                //begin row 5
                iText.Layout.Element.Cell cell51 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" Material: " + finalReading.Material +" Kgs"));
                iText.Layout.Element.Cell cell52 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14).SetMinHeight(15)
                   .Add(new Paragraph(" Cost Per Tonne: " + finalReading.Tonage));
                //end ro5
                table.AddCell(cell11);
                table.AddCell(cell12);
                table.AddCell(cell21);
                table.AddCell(cell22);
                table.AddCell(cell31);
                table.AddCell(cell32);
                table.AddCell(cell41);
                table.AddCell(cell42);
                table.AddCell(cell51);
                table.AddCell(cell52);
                document.Add(table);
                document.Add(newline);
                //Table 2 calculations
                Table table1 = new Table(4, false);
                //First Row
                iText.Layout.Element.Cell cell61 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(100)
                   .SetMaxWidth(132)
                   .SetFontSize(14)
                   .SetMinHeight(80)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph("Lorry Number: \n\n\n " +
                   "" + finalReading.Code));
                iText.Layout.Element.Cell cell62 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(130)
                   .SetMaxWidth(132)
                   .SetMinHeight(80)
                   .SetFontSize(14)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph("Gross Weight (M. Ton): \n Empty Lorry: \n" +
                   "" + finalReading.GrossWeight/1000));
                iText.Layout.Element.Cell cell71 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(130)
                   .SetMaxWidth(132)
                   .SetMinHeight(80)
                   .SetFontSize(14)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph("Tare Weight: (M. Ton): \n Loaded Lorry: \n" +
                   "" + finalReading.TareWeight/1000));
                iText.Layout.Element.Cell cell72 = new iText.Layout.Element.Cell(1, 1)
                   .SetMinWidth(130)
                   .SetMaxWidth(132)
                   .SetMinHeight(80)
                   .SetFontSize(14)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .Add(new Paragraph("NetWeight (M. Ton): \n\n\n" +
                   "" + finalReading.Tonage));
                //End Row 1
                table1.AddCell(cell61);
                table1.AddCell(cell62);
                table1.AddCell(cell71);
                table1.AddCell(cell72);
                //end table 2
                //Add Signatures
                Paragraph footer = new Paragraph("Sales Representative: .................................. " + finalReading.SysLogins +" Date: "+ DateTime.Now.ToShortDateString())
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14);
                Paragraph footer1 = new Paragraph("Accounts Representative: .............................. " + finalReading.SysLogins +" Date: " + DateTime.Now.ToShortDateString())
                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(14);
                //end signatures
                document.Add(table1);
                document.Add(newline);
                document.Add(footer);
                document.Add(footer1);
                document.Close();
                indicato.IsVisible = false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Report Test", ex.Message, "OK");
            }
            //ReportViewer reportViewer = new();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //LocalReport localReport = reportViewer.LocalReport;
            
            
            await Shell.Current.DisplayAlert("Success!!!", "Receipt has been generated: " + finalReading.Ticket, "Continue");
            hide1.IsVisible = false;
            hide3.HeightRequest = 510;
            hide3.IsVisible = true;
            hide2.IsVisible = false;
            hide4.IsVisible = false;
        }
    }
}