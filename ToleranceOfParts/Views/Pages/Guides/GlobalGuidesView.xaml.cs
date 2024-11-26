using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OPP.AppData.Guides;
using OPP.Navigation;
using OPP.ViewModels.Guides.GlobalGuides;
using ToleranceOfParts.Views.Pages.Guides.Edits;

namespace ToleranceOfParts.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для GlobalGuidesView.xaml
    /// </summary>
    public partial class GlobalGuidesView : Page
    {
        public ObservableCollection<GlobalLock> GlobalLocks { get; set; } = new ObservableCollection<GlobalLock>();
        public ObservableCollection<GlobalAssembly> GlobalAssemblies { get; set; } = new ObservableCollection<GlobalAssembly>();
        public ObservableCollection<GlobalPart> GlobalParts { get; set; } = new ObservableCollection<GlobalPart>();

        public object SelectedNode { get; set; }

        public GlobalGuidesView()
        {

            InitializeComponent();

            LoadGlobal();
        }

        private double _imageScale = 1.0; // Начальный масштаб
        private const double MinScale = 1.0; // Минимальный масштаб
        private const double MaxScale = 3.0; // Максимальный масштаб
        private const double ScaleStep = 0.1; // Шаг изменения масштаба

        private Point _startPoint;
        private bool _isDragging = false;
        private TranslateTransform _transform = new TranslateTransform();


        private void PopupImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Получаем позицию мыши относительно Popup
            var mousePosition = e.GetPosition(PopupImage);

            // Расчет изменения масштаба в зависимости от прокрутки
            if (e.Delta > 0)
            {
                _imageScale += ScaleStep;
            }
            else if (e.Delta < 0)
            {
                _imageScale -= ScaleStep;
            }

            // Ограничиваем масштаб минимальным и максимальным значением
            if (_imageScale < MinScale) _imageScale = MinScale;
            if (_imageScale > MaxScale) _imageScale = MaxScale;

            // Применяем новый масштаб
            ApplyScale(mousePosition);
        }

        private void ApplyScale(Point mousePosition)
        {
            // Получаем текущие размеры изображения
            double imageWidth = PopupImage.ActualWidth;
            double imageHeight = PopupImage.ActualHeight;

            // Рассчитываем точку масштабирования относительно текущих размеров
            double offsetX = (mousePosition.X / imageWidth);
            double offsetY = (mousePosition.Y / imageHeight);

            // Применяем масштабирование с учетом мыши
            PopupImage.RenderTransform = new ScaleTransform(_imageScale, _imageScale);

            // Устанавливаем точку масштабирования относительно мыши
            PopupImage.RenderTransformOrigin = new Point(offsetX, offsetY);
        }

        private async void LoadGlobal()
        {
            try
            {
                await Task.Run(async () =>
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var @lock in Lock.GetAll())
                        {
                            GlobalLocks.Add(new GlobalLock(@lock));
                        }
                    });
                });

                await Task.Run(async () =>
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var assembly in Assembly.GetAll())
                        {
                            GlobalAssemblies.Add(new GlobalAssembly(assembly));
                        }
                    });
                });

                await Task.Run(async () =>
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var part in Part.GetAll())
                        {
                            GlobalParts.Add(new GlobalPart(part));
                        }
                    });
                });

            }
            catch { }
        }

        private void OpenNodeExternal(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is GlobalPackaging packaging)
            {
                NavigationManager.Navigate(new EditPackaging(packaging));
            }
            else if (sender is Button button2 && button2.DataContext is GlobalLock lockNode)
            {
                NavigationManager.Navigate(new EditLock(lockNode));
            }
            else if (sender is Button button3 && button3.DataContext is GlobalAssembly assembly)
            {
                NavigationManager.Navigate(new EditAssembly(assembly));
            }
            else if (sender is Button button4 && button4.DataContext is GlobalPart part)
            {
                NavigationManager.Navigate(new EditPart(part));
            }
        }

        private void RootLocksControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as DataGrid;
            var item = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (item != null)
            {
                var selectedNode = listBox.ItemContainerGenerator.ItemFromContainer(item);
                OnNodeSelected(selectedNode, 1, MainGrid);
            }
        }

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }

        private void OnNodeSelected(object selectedNode, int level, Grid targetGrid)
        {
            SelectedNode = selectedNode;

            // Удаляем все последующие уровни, если узел выбран на предыдущем уровне
            while (targetGrid.ColumnDefinitions.Count > level)
            {
                targetGrid.ColumnDefinitions.RemoveAt(level);
                RemoveChildrenFromGrid(targetGrid, level);
            }

            // Получаем дочерние элементы для выбранного узла
            NodeChildren children = GetChildrenForNode(selectedNode);

            // Добавляем новый ColumnDefinition для текущего уровня
            targetGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Auto),
                MinWidth = 200
            });

            // Создаем новый Grid для дочерних элементов
            var childGrid = new Grid();
            Grid.SetColumn(childGrid, level);
            targetGrid.Children.Add(childGrid);

            // Считаем необходимое количество строк (2 на категорию)
            int numberOfRowsNeeded = children.Locks.Any() ? 2 : 0;
            numberOfRowsNeeded += children.Assemblies.Any() ? 2 : 0;
            numberOfRowsNeeded += children.Parts.Any() ? 2 : 0;
            numberOfRowsNeeded += children.Props.Any() ? 2 : 0;

            // Добавляем строки в новый Grid
            for (int i = 0; i < numberOfRowsNeeded; i++)
            {
                childGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }

            int rowIndex = 0; // Индекс строки для размещения элементов

            if (children.Locks.Any())
            {
                childGrid.Children.Add(CreateHeaderTextBlock("Изделия", rowIndex)); rowIndex++;
                childGrid.Children.Add(CreateDataGrid(children.Locks, "Изделие", rowIndex, level, numberOfRowsNeeded, targetGrid)); rowIndex++;
            }

            if (children.Assemblies.Any())
            {
                childGrid.Children.Add(CreateHeaderTextBlock("Сбор.ед.", rowIndex)); rowIndex++;
                childGrid.Children.Add(CreateDataGrid(children.Assemblies, "Сбор.ед.", rowIndex, level, numberOfRowsNeeded, targetGrid)); rowIndex++;
            }

            if (children.Parts.Any())
            {
                childGrid.Children.Add(CreateHeaderTextBlock("Детали", rowIndex)); rowIndex++;
                childGrid.Children.Add(CreateDataGrid(children.Parts, "Деталь", rowIndex, level, numberOfRowsNeeded, targetGrid)); rowIndex++;
            }

            if (children.Props.Any())
            {
                childGrid.Children.Add(CreateHeaderTextBlock("Доп. номенклатура", rowIndex)); rowIndex++;
                childGrid.Children.Add(CreateDataGrid(children.Props, "Деталь", rowIndex, level, numberOfRowsNeeded, targetGrid)); rowIndex++;
            }

            ToleranceGrid.Children.Clear();

            // Если это GlobalAssembly или GlobalPart, добавляем DataGrid в ToleranceGrid
            if (selectedNode is GlobalAssembly || selectedNode is GlobalPart)
            {

                ToleranceGrid.MinWidth = 300;

                var toleranceTextBlock = CreateHeaderTextBlock("Допуски для " + (selectedNode is GlobalAssembly ? ((GlobalAssembly)selectedNode).Code : ((GlobalPart)selectedNode).Code));
                ToleranceGrid.Children.Add(toleranceTextBlock);
                Grid.SetRow(toleranceTextBlock, 0);

                // DataGrid для чертежных размеров и допусков
                var toleranceDataGrid = new DataGrid
                {
                    IsReadOnly = true,
                    AutoGenerateColumns = false,
                    Margin = new Thickness(5),
                    ItemsSource = selectedNode is GlobalAssembly ? AssemblyTolerance.GetByAssembly((GlobalAssembly)selectedNode) : PartTolerance.GetByPart((GlobalPart)selectedNode),
                    MaxHeight = 350
                };

                // Создание колонок
                toleranceDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Чертежный размер",
                    Binding = new Binding("PlanSize")
                });
                toleranceDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Допускаемый размер",
                    Binding = new Binding("AllowedSize")
                });
                toleranceDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Примечание",
                    Binding = new Binding("Description")
                });

                // Добавляем в ToleranceGrid (Row = 1)
                ToleranceGrid.Children.Add(toleranceDataGrid);
                Grid.SetRow(toleranceDataGrid, 1);

                // Создаем изображение и добавляем обработчик для клика
                var image = new Image
                {
                    Source = selectedNode is GlobalAssembly
                        ? Assembly.GetImageAsBitmap((GlobalAssembly)selectedNode)
                        : Part.GetImageAsBitmap((GlobalPart)selectedNode),
                    Margin = new Thickness(5),
                    Cursor = Cursors.Hand,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Stretch = Stretch.Uniform
                };

                // Обработчик клика по изображению
                image.MouseLeftButtonUp += (s, e) =>
                {
                    // Устанавливаем изображение в Popup и открываем его
                    PopupImage.Source = image.Source;
                    ImagePopup.IsOpen = true;
                };


                // Добавляем изображение в ToleranceGrid (Row = 2)
                ToleranceGrid.Children.Add(image);
                Grid.SetRow(image, 2);

            }
        }


        private TextBlock CreateHeaderTextBlock(string text, int rowIndex)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                Margin = new Thickness(15, 5, 5, 5),
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };
            Grid.SetRow(textBlock, rowIndex);
            return textBlock;
        }

        private TextBlock CreateHeaderTextBlock(string text)
        {
            var textBlock = new TextBlock
            {
                Text = text,
                Margin = new Thickness(15, 5, 5, 5),
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };
            return textBlock;
        }

        private DataGrid CreateDataGrid(IEnumerable<dynamic> itemsSource, string headerText, int rowIndex, int level, int rowCount, Grid targetGrid)
        {
            var dataGrid = new DataGrid
            {
                ItemsSource = itemsSource,
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                IsReadOnly = true,
                Margin = new Thickness(5)
            };

            // Обработка события клика по строке
            dataGrid.MouseUp += (s, e) =>
            {
                var clickedItem = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
                if (clickedItem != null)
                {
                    var itemData = dataGrid.ItemContainerGenerator.ItemFromContainer(clickedItem);
                    OnNodeSelected(itemData, level + 1, targetGrid);
                }
            };

            // Колонка для динамического контента с кнопкой
            var templateColumn = new DataGridTemplateColumn
            {
                Header = headerText
            };
            var cellTemplate = new DataTemplate();

            // Создаем Grid для DataTemplate
            var gridFactory = new FrameworkElementFactory(typeof(Grid));

            // Определяем колонки для Grid
            var columnDef1 = new FrameworkElementFactory(typeof(ColumnDefinition));
            columnDef1.SetValue(ColumnDefinition.WidthProperty, new GridLength(0, GridUnitType.Auto));
            gridFactory.AppendChild(columnDef1);

            var columnDef2 = new FrameworkElementFactory(typeof(ColumnDefinition));
            gridFactory.AppendChild(columnDef2);

            switch (itemsSource)
            {
                case IEnumerable<GlobalProp>:

                    break;

                default:

                    // Кнопка для открытия узла
                    var buttonFactory = new FrameworkElementFactory(typeof(Button));
                    buttonFactory.SetValue(Button.HeightProperty, 30.0);
                    buttonFactory.SetValue(Button.MarginProperty, new Thickness(-5));
                    buttonFactory.SetValue(Button.PaddingProperty, new Thickness(2));
                    buttonFactory.SetResourceReference(Button.StyleProperty, "MaterialDesignFlatButton");
                    buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(OpenNodeExternal)); // Событие клика
                    buttonFactory.SetValue(Button.DataContextProperty, new Binding(".")); // Установите DataContext кнопки равным текущему элементу


                    // Изображение для кнопки
                    var imageFactory = new FrameworkElementFactory(typeof(Image));
                    imageFactory.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Assets/OpenExternally.png", UriKind.Absolute)));
                    imageFactory.SetValue(Image.OpacityProperty, 0.6);
                    imageFactory.SetValue(Image.MarginProperty, new Thickness(0, 0, 5, 0));

                    buttonFactory.AppendChild(imageFactory);
                    gridFactory.AppendChild(buttonFactory); // Добавляем кнопку в Grid
                    break;

            }

            // StackPanel для текста
            var stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
            stackPanelFactory.SetValue(StackPanel.MarginProperty, new Thickness(10, 0, 0, 0));
            stackPanelFactory.SetValue(Grid.ColumnProperty, 1); // Устанавливаем колонку

            // TextBlock для кода
            var textBlockCodeFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockCodeFactory.SetBinding(TextBlock.TextProperty, new Binding("Code"));
            textBlockCodeFactory.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);

            // TextBlock для имени
            var textBlockNameFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockNameFactory.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            // Добавляем TextBlock в StackPanel
            stackPanelFactory.AppendChild(textBlockCodeFactory);
            stackPanelFactory.AppendChild(textBlockNameFactory);

            // Добавляем StackPanel в Grid
            gridFactory.AppendChild(stackPanelFactory);

            // Устанавливаем DataTemplate
            cellTemplate.VisualTree = gridFactory;
            templateColumn.CellTemplate = cellTemplate;

            // Колонка для количества
            var countColumn = new DataGridTextColumn
            {
                Header = "Кол-во",
                Binding = new Binding("Count"),
                FontWeight = FontWeights.Bold
            };

            //Максимальная высота
            var maxHeightBinding = new MultiBinding
            {
                Converter = new MaxHeightConverter()
            };

            // Привязываем к ActualHeight MainGrid
            var heightBinding = new Binding("ActualHeight")
            {
                Source = MainGrid
            };
            maxHeightBinding.Bindings.Add(heightBinding);

            // Привязываем к числу строк
            var rowCountBinding = new Binding
            {
                Source = Math.Max(1, rowCount / 2 + 1)
            };
            maxHeightBinding.Bindings.Add(rowCountBinding);

            // Применяем MultiBinding к MaxHeight ListBox
            BindingOperations.SetBinding(dataGrid, FrameworkElement.MaxHeightProperty, maxHeightBinding);

            // Добавляем колонки в DataGrid
            dataGrid.Columns.Add(templateColumn);
            dataGrid.Columns.Add(countColumn);
            Grid.SetRow(dataGrid, rowIndex);
            return dataGrid;
        }


        private void RemoveChildrenFromGrid(Grid targetGrid, int level)
        {
            for (int i = targetGrid.Children.Count - 1; i >= 0; i--)
            {
                var element = targetGrid.Children[i];
                if (Grid.GetColumn(element) >= level)
                {
                    targetGrid.Children.RemoveAt(i);
                }
            }
        }


        private NodeChildren GetChildrenForNode(object node)
        {
            var result = new NodeChildren();

            switch (node)
            {
                case GlobalPackaging packaging:
                    result.Locks = packaging.Locks;
                    result.Assemblies = packaging.Assemblies;
                    result.Parts = packaging.Parts;
                    result.Props = packaging.Props;
                    break;

                case GlobalLock lockNode:
                    result.Assemblies = lockNode.Assemblies;
                    result.Parts = lockNode.Parts;
                    result.Props = lockNode.Props;
                    break;

                case GlobalAssembly assemblyNode:
                    result.Assemblies = assemblyNode.Assemblies;
                    result.Parts = assemblyNode.Parts;
                    break;

                case GlobalPart part:
                    result.Blanks = part.Blanks;
                    break;

                default:
                    break;
            }

            return result;
        }

        private DataTemplate GetTemplateForNodeType(object node)
        {
            switch (node)
            {
                case GlobalPackaging _:
                    return (DataTemplate)FindResource("GlobalPackagingTemplate");

                case GlobalLock _:
                    return (DataTemplate)FindResource("GlobalLockTemplate");

                case GlobalAssembly _:
                    return (DataTemplate)FindResource("GlobalAssemblyTemplate");

                case GlobalPart _:
                    return (DataTemplate)FindResource("GlobalPartTemplate");

                case Blank _:
                    return (DataTemplate)FindResource("GlobalBlankTemplate");

                default:
                    return null;
            }
        }

        private void RootAssembliesControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as DataGrid;
            var item = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (item != null)
            {
                var selectedNode = listBox.ItemContainerGenerator.ItemFromContainer(item);
                OnNodeSelected(selectedNode, 1, AssembliesGrid);
            }
        }

        private void RootPartsControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as DataGrid;
            var item = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (item != null)
            {
                var selectedNode = listBox.ItemContainerGenerator.ItemFromContainer(item);
                OnNodeSelected(selectedNode, 1, PartsGrid);
            }
        }
    }

    public class NodeChildren
    {
        public IEnumerable<GlobalLock> Locks { get; set; } = Enumerable.Empty<GlobalLock>();
        public IEnumerable<GlobalAssembly> Assemblies { get; set; } = Enumerable.Empty<GlobalAssembly>();
        public IEnumerable<GlobalPart> Parts { get; set; } = Enumerable.Empty<GlobalPart>();
        public IEnumerable<GlobalProp> Props { get; set; } = Enumerable.Empty<GlobalProp>();
        public IEnumerable<Blank> Blanks { get; set; } = Enumerable.Empty<Blank>();
    }

    public class MaxHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 &&
                values[0] is double totalHeight &&
                values[1] is int rowCount &&
                rowCount > 0)
            {
                return totalHeight / rowCount;
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}