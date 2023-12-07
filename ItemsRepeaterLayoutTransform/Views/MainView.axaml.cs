using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media.Transformation;

namespace ItemsRepeaterLayoutTransform.Views;

public partial class MainView : UserControl
{
    private readonly double _minZoom = 0.15;
    private readonly double _maxZoom = 500.0;
    private readonly double _zoomInDelta = 1.1;
    private readonly double _zoomOutDelta = 0.91;

    private LayoutTransformControl? _layoutTransformControl;

    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _layoutTransformControl = this.Get<LayoutTransformControl>("PART_LayoutTransformControl");

        _layoutTransformControl.AddHandler(PointerWheelChangedEvent, _onPointerWheelChangedHandler);
    }

    private void _onPointerWheelChangedHandler(object? sender, PointerWheelEventArgs e)
    {
        if (e.KeyModifiers != KeyModifiers.Control || e.Delta.Y == 0)
        {
            return;
        }

        ZoomTo(e);
        e.Handled = true;
    }

    private void ZoomTo(PointerWheelEventArgs e)
    {
        if (_layoutTransformControl is null)
        {
            return;
        }

        TransformOperations.Builder builder = TransformOperations.CreateBuilder(2);
        Matrix? matrix = _layoutTransformControl.LayoutTransform?.Value;
        if (matrix.HasValue)
        {
            builder.AppendMatrix(matrix.Value);
        }

        double scale = e.Delta.Y < 0 ? _zoomOutDelta : _zoomInDelta;
        builder.AppendScale(scale, scale);
        var newTransform = builder.Build();

        if (newTransform.Value.M11 < _minZoom || newTransform.Value.M11 > _maxZoom)
        {
            return;
        }

        _layoutTransformControl.LayoutTransform = newTransform;
    }
}
