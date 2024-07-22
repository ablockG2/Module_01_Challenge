namespace Module_01_Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Module01Challenge : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here
            double number1 = 250;
            double startingElevation = 0;
            double floorHeight = 15;


            Transaction t = new Transaction(doc);
            t.Start("Creating levels up to 250");

            for (int floorLevel = 1; floorLevel <= number1; floorLevel++)
            {
                Level newLevel1 = Level.Create(doc, startingElevation);
                newLevel1.Name = "New Level " + floorLevel.ToString();
                startingElevation += floorHeight;

                if (floorLevel == 251)
                {
                    break;
                }
                else if (floorLevel % 3 == 0 && floorLevel % 5 == 0)
                {
                    FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                    collector1.OfCategory(BuiltInCategory.OST_TitleBlocks);

                    ViewSheet newSheet1 = ViewSheet.Create(doc, collector1.FirstElementId());
                    newSheet1.Name = "FIZZBUZZ_" + floorLevel.ToString();
                    newSheet1.SheetNumber = floorLevel.ToString();

                    FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                    collector2.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector2)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                            break;
                        }
                    }

                    ViewPlan newPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel1.Id);
                    newPlan.Name = "FIZZBUZZ_" + floorLevel.ToString();

                    XYZ inspoint = new XYZ(1, 1, 0);

                    Viewport newViewport1 = Viewport.Create(doc, newSheet1.Id, newPlan.Id, inspoint);
                }
                else if (floorLevel % 3 == 0)
                {
                    FilteredElementCollector collector3 = new FilteredElementCollector(doc);
                    collector3.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector3)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = curVFT;
                            break;
                        }
                    }

                    ViewPlan newPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel1.Id);
                    newPlan.Name = "FIZZ_" + floorLevel.ToString();
                }
                else if (floorLevel % 5 == 0)
                {
                    FilteredElementCollector collector4 = new FilteredElementCollector(doc);
                    collector4.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType ceilingPlanVFT = null;
                    foreach (ViewFamilyType curVFT in collector4)
                    {
                        if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                        {
                            ceilingPlanVFT = curVFT;
                            break;
                        }
                    }

                    ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel1.Id);
                    newCeilingPlan.Name = "BUZZ_" + floorLevel.ToString();
                }

            }

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";

            Utils.ButtonDataClass myButtonData1 = new Utils.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Blue_32,
                Properties.Resources.Blue_16,
                "This is a tooltip for Button 1");

            return myButtonData1.Data;
        }
    }

}
